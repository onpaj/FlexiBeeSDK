using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices;

public class ReceivedInvoiceSearchResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("faktura-prijata")]
    public List<ReceivedInvoiceSearchDto> ReceivedInvoices { get; set; }
}