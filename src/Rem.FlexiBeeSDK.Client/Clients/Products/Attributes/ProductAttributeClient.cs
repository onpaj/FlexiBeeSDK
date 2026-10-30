using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Products.Attributes;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.Attributes;

public class ProductAttributeClient : ResourceClient, IProductAttributeClient
{
    private readonly ILogger<ProductAttributeClient> _logger;

    public ProductAttributeClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<ProductAttributeClient> logger
        ) : base(connection, httpClientFactory, resultHandler, logger)
    {
        _logger = logger;
    }

    protected override string ResourceIdentifier => Agenda.ProductAttribute;

    public async Task<OperationResult<ProductAttributeFlexiDto>> GetAsync(ProductReference product, int attributeTypeId, CancellationToken cancellationToken = default)
    {
        Validate(product, attributeTypeId);

        var rows = await FindRowsAsync(product, attributeTypeId, cancellationToken);
        if (!rows.IsSuccess)
            return Failed<IList<ProductAttributeFlexiDto>, ProductAttributeFlexiDto>(rows);

        if (rows.Result!.Count > 1)
            return Failed<ProductAttributeFlexiDto>(HttpStatusCode.Conflict, DuplicateMessage(product, attributeTypeId, rows.Result));

        return rows.Result.Count == 0
            ? new OperationResult<ProductAttributeFlexiDto>(HttpStatusCode.OK)
            : new OperationResult<ProductAttributeFlexiDto>(HttpStatusCode.OK, rows.Result[0]);
    }

    public async Task<OperationResult<OperationResultDetail>> SetAsync(ProductReference product, int attributeTypeId, ProductAttributeValue value, CancellationToken cancellationToken = default)
    {
        Validate(product, attributeTypeId);
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        var typeCheck = await CheckValueTypeAsync(attributeTypeId, value, cancellationToken);
        if (!typeCheck.IsSuccess)
            return Failed<ProductAttributeFlexiDto, OperationResultDetail>(typeCheck);

        var rows = await FindRowsAsync(product, attributeTypeId, cancellationToken);
        if (!rows.IsSuccess)
            return Failed<IList<ProductAttributeFlexiDto>, OperationResultDetail>(rows);

        if (rows.Result!.Count > 1)
            return Failed<OperationResultDetail>(HttpStatusCode.Conflict, DuplicateMessage(product, attributeTypeId, rows.Result));

        var request = rows.Result.Count == 0
            ? ProductAttributeWriteRequest.Create(product, attributeTypeId, value)
            : ProductAttributeWriteRequest.Update(rows.Result[0].Id, value);

        return await WriteAsync(request, cancellationToken);
    }

    public async Task<OperationResult<OperationResultDetail>> ClearAsync(ProductReference product, int attributeTypeId, CancellationToken cancellationToken = default)
    {
        Validate(product, attributeTypeId);

        var rows = await FindRowsAsync(product, attributeTypeId, cancellationToken);
        if (!rows.IsSuccess)
            return Failed<IList<ProductAttributeFlexiDto>, OperationResultDetail>(rows);

        if (rows.Result!.Count == 0)
            return new OperationResult<OperationResultDetail>(HttpStatusCode.OK);

        var deletes = rows.Result.Select(r => ProductAttributeWriteRequest.Delete(r.Id)).ToList();
        return await WriteAsync(deletes, cancellationToken);
    }

    /// <summary>
    /// Posts a write and reports every failure through the result: transport errors, timeouts, FlexiBee's error
    /// message on rejected requests, and a <c>success:false</c> body delivered with a 2xx status.
    /// </summary>
    private async Task<OperationResult<OperationResultDetail>> WriteAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
    {
        OperationResult<OperationResultDetail> result;
        try
        {
            result = await PostAsync(request, cancellationToken: cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "FlexiBee write failed");
            return Failed<OperationResultDetail>(HttpStatusCode.ServiceUnavailable, ex.Message);
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex, "FlexiBee write timed out");
            return Failed<OperationResultDetail>(HttpStatusCode.GatewayTimeout, "FlexiBee write timed out.");
        }

        if (!result.IsSuccess)
        {
            return Failed<OperationResultDetail>(result.StatusCode, result.GetErrorMessage() ?? result.Result?.Message ?? $"FlexiBee returned {(int)result.StatusCode}.");
        }

        // A parse error in the base client leaves the status 2xx but sets ErrorMessage.
        if (result.ErrorMessage != null || result.Result?.Success == "false")
        {
            return Failed<OperationResultDetail>(HttpStatusCode.BadGateway, result.ErrorMessage ?? result.Result?.Message ?? "FlexiBee reported a failure.");
        }

        return result;
    }

    private async Task<OperationResult<ProductAttributeFlexiDto>> CheckValueTypeAsync(int attributeTypeId, ProductAttributeValue value, CancellationToken cancellationToken)
    {
        var query = new QueryBuilder()
            .ById(attributeTypeId)
            .WithCustomDetail(ProductAttributeTypeFlexiDto.DetailFields)
            .Build();

        var types = await FetchAsync<ProductAttributeTypeFlexiDto>(GetUri(query, Agenda.ProductAttributeType), Agenda.ProductAttributeType, cancellationToken);
        if (!types.IsSuccess)
            return Failed<IList<ProductAttributeTypeFlexiDto>, ProductAttributeFlexiDto>(types);

        var type = types.Result!.FirstOrDefault();
        if (type == null)
            return Failed<ProductAttributeFlexiDto>(HttpStatusCode.NotFound, $"Attribute type {attributeTypeId} was not found.");

        if (type.ValueTypeKey != value.RequiredTypeKey)
        {
            return Failed<ProductAttributeFlexiDto>(HttpStatusCode.BadRequest,
                $"Attribute type {attributeTypeId} ({type.Code}) has value type '{type.ValueTypeKey}', cannot store a {value.Kind} value ('{value.RequiredTypeKey}' expected).");
        }

        return new OperationResult<ProductAttributeFlexiDto>(HttpStatusCode.OK);
    }

    private Task<OperationResult<IList<ProductAttributeFlexiDto>>> FindRowsAsync(ProductReference product, int attributeTypeId, CancellationToken cancellationToken)
    {
        // The filter must be part of the URL path; a ?filter= query parameter is silently ignored by FlexiBee
        // and would return every row of the evidence.
        var productOperand = product.Code != null
            ? $"'{Uri.EscapeDataString("code:" + product.Code)}'"
            : product.Id!.Value.ToString();

        var query = new QueryBuilder()
            .Raw($"(cenik={productOperand}%20and%20typAtributu={attributeTypeId})")
            .WithCustomDetail(ProductAttributeFlexiDto.DetailFields)
            .WithNoLimit()
            .Build();

        return FetchAsync<ProductAttributeFlexiDto>(GetUri(query, ResourceIdentifier), ResourceIdentifier, cancellationToken);
    }

    private async Task<OperationResult<IList<T>>> FetchAsync<T>(string uri, string resultIdentifier, CancellationToken cancellationToken)
    {
        HttpStatusCode statusCode;
        string body;
        try
        {
            _logger.LogDebug("HttpRequest: GET {Uri}", uri);
            var response = await GetClient().GetAsync(uri, cancellationToken);
            statusCode = response.StatusCode;
            body = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("HttpResult: {StatusCode}", statusCode);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "FlexiBee request failed: GET {Uri}", uri);
            return Failed<IList<T>>(HttpStatusCode.ServiceUnavailable, ex.Message);
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex, "FlexiBee request timed out: GET {Uri}", uri);
            return Failed<IList<T>>(HttpStatusCode.GatewayTimeout, "FlexiBee request timed out.");
        }

        if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.Ambiguous)
        {
            _logger.LogError("FlexiBee GET {Uri} returned {StatusCode}: {Body}", uri, statusCode, body);
            return Failed<IList<T>>(statusCode, ExtractMessage(body) ?? $"FlexiBee returned {(int)statusCode}.");
        }

        try
        {
            var rows = (JObject.Parse(body).SelectToken($"winstrom.{resultIdentifier}") as JArray)?.ToObject<List<T>>();
            return rows == null
                ? Failed<IList<T>>(HttpStatusCode.BadGateway, $"Unexpected FlexiBee response, '{resultIdentifier}' is missing.")
                : new OperationResult<IList<T>>(statusCode, rows);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Unable to parse FlexiBee response of GET {Uri}: {Body}", uri, body);
            return Failed<IList<T>>(HttpStatusCode.BadGateway, $"Unable to parse FlexiBee response: {ex.Message}");
        }
    }

    private static string? ExtractMessage(string body)
    {
        try
        {
            return JObject.Parse(body).SelectToken("winstrom.message")?.ToString();
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static void Validate(ProductReference product, int attributeTypeId)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        if (attributeTypeId <= 0)
            throw new ArgumentOutOfRangeException(nameof(attributeTypeId), attributeTypeId, "Attribute type id must be positive.");
    }

    private static string DuplicateMessage(ProductReference product, int attributeTypeId, IList<ProductAttributeFlexiDto> rows) =>
        $"Product {product} has {rows.Count} rows of attribute type {attributeTypeId} (ids {string.Join(", ", rows.Select(r => r.Id))}); expected at most one.";

    private static OperationResult<T> Failed<T>(HttpStatusCode statusCode, string message) =>
        new OperationResult<T>(statusCode, message);

    private static OperationResult<TTarget> Failed<TSource, TTarget>(OperationResult<TSource> source) =>
        new OperationResult<TTarget>(source.StatusCode, source.ErrorMessage ?? $"FlexiBee returned {(int)source.StatusCode}.");
}
