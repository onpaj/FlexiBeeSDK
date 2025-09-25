using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.IssuedOrders;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.IssuedOrders;

public class IssuedOrdersClient : ResourceClient, IIssuedOrdersClient
{
    public IssuedOrdersClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler  resultHandler,
        ILogger<IssuedOrdersClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
    }

    protected override string ResourceIdentifier => Agenda.IssuedOrders;

    protected override string? RequestIdentifier => null;

    public async Task<IReadOnlyList<IssuedOrderFlexiDto>> GetAsync(DateTime dateFrom, DateTime dateTo, int? documentTypeId = null, int limit = 0, int skip = 0,
        CancellationToken cancellationToken = default)
    {
        var queryDoc = new IssuedOrderRequest(dateFrom, dateTo, documentTypeId)
        {
            Limit = limit,
            Start = skip,
        };

        var query = new FlexiQuery();
        var result = await PostAsync<IssuedOrderRequest, IssuedOrderResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result.ObjednavkaVydana ?? new List<IssuedOrderFlexiDto>();
    }
    
    public async Task<IssuedOrderFlexiDto?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var queryDoc = new IssuedOrderRequest(id)
        {
        };

        var query = new FlexiQuery();
        var result = await PostAsync<IssuedOrderRequest, IssuedOrderResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.ObjednavkaVydana?.FirstOrDefault();
    }

    public async Task<IssuedOrderFlexiDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var queryDoc = new IssuedOrderRequest(code)
        {
        };

        var query = new FlexiQuery();
        var result = await PostAsync<IssuedOrderRequest, IssuedOrderResult>(queryDoc, query, cancellationToken: cancellationToken);

        return result?.Result?.ObjednavkaVydana?.FirstOrDefault();
    }

    public Task<OperationResult<OperationResultDetail>> SaveAsync(CreateIssuedOrderFlexiDto issuedOrder, CancellationToken cancellationToken = default)
        => PostAsync(new CreateIssuedOrderItemEnvelopeFlexiDto(issuedOrder), cancellationToken: cancellationToken);

    public Task<OperationResult<OperationResultDetail>> FinalizeAsync(FinalizeIssuedOrderFlexiDto order, CancellationToken cancellationToken = default) 
        => PostAsync(new FinalizeIssuedOrderEnvelopeFlexiDto(order), cancellationToken: cancellationToken);
}