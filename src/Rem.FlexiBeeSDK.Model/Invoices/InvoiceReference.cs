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
    public double Ammount { get; set; }

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

    [JsonProperty("b@evidencePath")]
    public string BEvidencePath { get; set; }

    [JsonProperty("b@internalId")]
    public int ReferenceBId { get; set; }
}

