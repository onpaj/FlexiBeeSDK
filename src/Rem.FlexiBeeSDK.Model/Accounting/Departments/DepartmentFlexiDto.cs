using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Accounting.Departments;

public class DepartmentFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("lastUpdate")]
    public DateTime LastUpdate { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }
}