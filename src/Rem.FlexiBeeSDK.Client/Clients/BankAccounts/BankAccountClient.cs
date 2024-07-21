using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.BankAccounts;

public class BankAccountClient: ResourceClient, IBankAccountClient
{
    private readonly FlexiBeeSettings _connection;

    public BankAccountClient(
        FlexiBeeSettings connection,
        IHttpClientFactory httpClientFactory,
        IResultHandler resultHandler,
        ILogger<BankAccountClient> logger
    )
        : base(connection, httpClientFactory, resultHandler, logger)
    {
        _connection = connection;
    }

    protected override string ResourceIdentifier => Agenda.BankAccount;

    // https://podpora.flexibee.eu/cs/articles/4731153-nacitani-bankovnich-vypisu
    public async Task<OperationResult<OperationResultDetail>> ImportStatement(int accountId, string data)
    {
        var client = GetClient();
        var bytes = Encoding.GetEncoding("iso-8859-2").GetBytes(data);
        var content = new StreamContent(new MemoryStream(bytes));
        content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        content.Headers.ContentType.CharSet = "iso-8859-2";
        var result = await client.PostAsync(GetImportUri(accountId.ToString()), content);
        var json = await result.Content.ReadAsStringAsync();

        JObject jo = JObject.Parse(json);
        return new OperationResult<OperationResultDetail>(result.StatusCode, jo["message"]?.ToString() ?? "");
    }
    
    
    private string GetImportUri(string accountId)
    {
        return $"{_connection.Server}/c/{_connection.Company}/{ResourceIdentifier}/{accountId}/nacteni-vypisu";
    }
}