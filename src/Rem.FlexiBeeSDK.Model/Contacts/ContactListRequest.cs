using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Contacts;

public class ContactListRequest
{
    public ContactListRequest(ContactType contactType) : this([contactType])
    {
        
    }

    public ContactListRequest(IEnumerable<ContactType> contactTypes)
    {
        Filter = $"typVztahuK in ({string.Join(",", contactTypes.Select(s => $"\"typVztahu.{GetContactTypeString(s)}\""))})";
    }

    private string GetContactTypeString(ContactType contactType)
    {
        return FromContactType(contactType);
    }

    private static string FromContactType(ContactType contactType)
    {
        switch (contactType)
        {
            case ContactType.Supplier:
                return "dodavatel";
            case ContactType.SupplierAndCustomer:
                return "odberDodav";
            case ContactType.Customer:
                return "odberatel";
            case ContactType.All:
                return "vsechno";
        }

        return "vsechno";
    }
    
    private static ContactType ToContactType(string contactType)
    {
        switch (contactType)
        {
            case "typVztahu.dodavatel":
                return ContactType.Supplier;
            case "typVztahu.odberDodav":
                return ContactType.SupplierAndCustomer;
            case "typVztahu.odberatel":
                return ContactType.Customer;
            case "typVztahu.vsechno":
                return ContactType.All;
        }

        return ContactType.Supplier;
    }

    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:kod,nazev,ulice,psc,mesto,tel,mobil,email,platceDph,nespolehlivyPlatce,poznam,popis,id,typVztahuK";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("order")] public string Order { get; set; } = "nazev";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";

    [JsonProperty("filter")] public string Filter { get; private set; }
}