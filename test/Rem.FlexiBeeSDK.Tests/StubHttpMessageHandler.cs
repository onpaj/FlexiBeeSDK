using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Rem.FlexiBeeSDK.Tests;

/// <summary>Answers requests from a callback and records them (with bodies read eagerly) for assertions.</summary>
public sealed class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<RecordedRequest, HttpResponseMessage> _respond;

    public StubHttpMessageHandler(Func<RecordedRequest, HttpResponseMessage> respond)
    {
        _respond = respond;
    }

    public List<RecordedRequest> Requests { get; } = new();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
        var recorded = new RecordedRequest(request.Method, request.RequestUri!.OriginalString, body);
        Requests.Add(recorded);
        return _respond(recorded);
    }

    public static HttpResponseMessage Json(HttpStatusCode status, string json) =>
        new(status) { Content = new StringContent(json) };

    public sealed record RecordedRequest(HttpMethod Method, string Url, string? Body);
}
