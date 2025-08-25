using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Contacts;

public class ContactListRequest
{
    public ContactListRequest(ContactType contactType)
    {
        Filter =
            $"typVztahuK in (\"typVztahu.{GetContactTypeString(contactType)}\")";
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
        }

        return "dodavatel";
    }
    
    private static ContactType ToContactType(string contactType)
    {
        switch (contactType)
        {
            case "typVztahu.dodavatel":
                return ContactType.Supplier;
        }

        return ContactType.Supplier;
    }

    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:kod,nazev,ulice,psc,mesto,tel,mobil,email,platceDph,nespolehlivyPlatce,poznam,id,typVztahuK";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("order")] public string Order { get; set; } = "nazev";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";

    [JsonProperty("filter")] public string Filter { get; private set; }
}