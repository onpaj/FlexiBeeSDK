using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class Kusovnik
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
        public string Nazev { get; set; }

        [JsonProperty("nazevA", NullValueHandling = NullValueHandling.Ignore)]
        public string NazevA { get; set; }

        [JsonProperty("nazevB", NullValueHandling = NullValueHandling.Ignore)]
        public string NazevB { get; set; }

        [JsonProperty("nazevC", NullValueHandling = NullValueHandling.Ignore)]
        public string NazevC { get; set; }

        [JsonProperty("mnoz", NullValueHandling = NullValueHandling.Ignore)]
        public string Mnoz { get; set; }

        [JsonProperty("hladina", NullValueHandling = NullValueHandling.Ignore)]
        public long? Hladina { get; set; }

        [JsonProperty("poradi", NullValueHandling = NullValueHandling.Ignore)]
        public long? Poradi { get; set; }

        [JsonProperty("cesta", NullValueHandling = NullValueHandling.Ignore)]
        public string Cesta { get; set; }

        [JsonProperty("otecCenik", NullValueHandling = NullValueHandling.Ignore)]
        public string OtecCenik { get; set; }

        [JsonProperty("otecCenik@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string OtecCenikRef { get; set; }

        [JsonProperty("otecCenik@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string OtecCenikShowAs { get; set; }

        [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
        public string Cenik { get; set; }

        [JsonProperty("cenik@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string CenikRef { get; set; }

        [JsonProperty("cenik@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string CenikShowAs { get; set; }

        [JsonProperty("otec", NullValueHandling = NullValueHandling.Ignore)]
        public string Otec { get; set; }
    }
}
