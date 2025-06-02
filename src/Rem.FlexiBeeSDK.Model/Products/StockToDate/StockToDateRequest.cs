using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Products.StockToDate;

public class StockToDateRequest
{
    [JsonProperty("add-row-count")] public bool AddRowCount { get; set; } = true;

    [JsonProperty("detail")]
    public string Detail { get; set; } =
        "custom:cenik(nazev,kod,id,baleniNazev1,baleniMj1,evidSarze,evidExpir),eanKod,id,mj1(nazev,kod,id),nazev,pozadavkyMJ,prumCena,skupZboz(nazev,kod,id),stavMJ,stavMJPozad,stitky,tuz";

    [JsonProperty("limit")] public int Limit { get; set; } = 0;

    [JsonProperty("start")] public int Start { get; set; } = 0;

    [JsonProperty("includes")]
    public string Includes { get; set; } =
        "/stav-skladu-k-datu/cenik,/stav-skladu-k-datu/mj1,/stav-skladu-k-datu/skupZboz";

    [JsonProperty("order")] public string Order { get; set; } = "id";

    [JsonProperty("use-internal-id")] public bool UseInternalId { get; set; } = true;

    [JsonProperty("no-ext-ids")] public bool NoExtIds { get; set; } = true;

    [JsonProperty("@version")] public string Version { get; set; } = "1.0";
}