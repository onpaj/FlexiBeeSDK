using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.BoM;

public class BomRequest
{
    [JsonIgnore]
    public string Code { get; set; }

    [JsonProperty("as-gui")]
    public string AsGui => "true";

    [JsonProperty("access-attribs")]
    public string AccessAttribs => "true";

    [JsonProperty("detail")]
    public string Detail => "custom:id,mnoz,hladina,poradi,cesta,cenik(id,kod,nazev,nakupCena,popis),nazev,otecCenik(id,kod,nazev)";

    [JsonProperty("limit")] public string Limit => "0";

    [JsonProperty("includes")]
    public string Includes = "/kusovnik/cenik,/kusovnik/cenik/cenik/mj1,/kusovnik/otec,/kusovnik/otecCenik";

    [JsonProperty("filter")] public string Filter { get; set; }

    [JsonProperty("use-internal-id")] public string UseInternalId => "true";

    [JsonProperty("no-ext-ids")] public string NoExtIds => "true";

    [JsonProperty("@version")] public string Version = "1.0";

    public BomRequest FindByParentCode(string code)
    {
        Code = code;
        Filter = $"otecCenik.kod eq \"{Code}\"";
        return this;
    }
    
    public BomRequest FindByIngredientCode(string code)
    {
        Code = code;
        Filter = $"cenik.kod eq \"{Code}\"";
        return this;
    }
}