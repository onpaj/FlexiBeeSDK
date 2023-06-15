using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class BoM
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("nazevA", NullValueHandling = NullValueHandling.Ignore)]
        public string NameA { get; set; }

        [JsonProperty("nazevB", NullValueHandling = NullValueHandling.Ignore)]
        public string NameB { get; set; }

        [JsonProperty("nazevC", NullValueHandling = NullValueHandling.Ignore)]
        public string NameC { get; set; }

        [JsonProperty("mnoz", NullValueHandling = NullValueHandling.Ignore)]
        public string Ammount { get; set; }

        [JsonProperty("hladina", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; set; }

        [JsonProperty("poradi", NullValueHandling = NullValueHandling.Ignore)]
        public long? Order { get; set; }

        [JsonProperty("cesta", NullValueHandling = NullValueHandling.Ignore)]
        public string Route { get; set; }

        [JsonProperty("otecCenik", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentPriceList { get; set; }

        [JsonProperty("otecCenik@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentPriceListRef { get; set; }

        [JsonProperty("otecCenik@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentPriceListShowAs { get; set; }

        [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceList { get; set; }

        [JsonProperty("cenik@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceListRef { get; set; }

        [JsonProperty("cenik@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceListShowAs { get; set; }

        [JsonProperty("otec", NullValueHandling = NullValueHandling.Ignore)]
        public string Parent { get; set; }
    }
}
