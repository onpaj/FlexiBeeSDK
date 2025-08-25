using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Contacts;

public class ContactListResult
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@rowCount")]
    public string RowCount { get; set; }

    [JsonProperty("adresar")]
    public List<ContactFlexiDto> Contacts { get; set; }
}