using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices;

public class InvoiceReference
{
    public const string ReferenceTypePayment = "typVazbyDokl.uhrada";
    
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("storno")]
    public bool Canceled { get; set; }

    [JsonProperty("castka")]
    public double Amount { get; set; }

    [JsonProperty("typVazbyK")]
    public string ReferenceType { get; set; }

    [JsonProperty("castkaMen")]
    public double CastkaMen { get; set; }

    [JsonProperty("mena")]
    public string Mena { get; set; }

    [JsonProperty("a")]
    public string ReferenceA { get; set; }

    [JsonProperty("a@internalId")]
    public int ReferenceAId { get; set; }

    [JsonProperty("b")]
    public string ReferenceB { get; set; }

    [JsonProperty("b@ref")]
    public string BRef { get; set; }

    // Extracts the agenda segment from BRef: "/c/{company}/{agenda}/{id}.json" → "{agenda}"
    public string BAgenda => BRef?.Split('/') is { Length: > 3 } parts ? parts[3] : null;

}

