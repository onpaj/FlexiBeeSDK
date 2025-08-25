using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Contacts;

public class ContactFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("ulice")]
    public string Street { get; set; }

    [JsonProperty("psc")]
    public string PostalCode { get; set; }

    [JsonProperty("mesto")]
    public string City { get; set; }

    [JsonProperty("tel")]
    public string Phone { get; set; }

    [JsonProperty("mobil")]
    public string Mobile { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("platceDph")]
    public bool VatPayer { get; set; }

    [JsonProperty("nespolehlivyPlatce")]
    public bool UnreliablePayer { get; set; }

    [JsonProperty("poznam")]
    public string Note { get; set; }
    
    [JsonProperty("typVztahuK")]
    public string ContactType { get; set; }

    [JsonProperty("typVztahuK@showAs")]
    public string ContactTypeName { get; set; }

}