using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class Adresar
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Kod { get; set; }

        [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
        public string Nazev { get; set; }

        [JsonProperty("ic", NullValueHandling = NullValueHandling.Ignore)]
        public string Ic { get; set; }

        [JsonProperty("dic", NullValueHandling = NullValueHandling.Ignore)]
        public string Dic { get; set; }

        [JsonProperty("ulice", NullValueHandling = NullValueHandling.Ignore)]
        public string Ulice { get; set; }

        [JsonProperty("mesto", NullValueHandling = NullValueHandling.Ignore)]
        public string Mesto { get; set; }

        [JsonProperty("psc", NullValueHandling = NullValueHandling.Ignore)]
        public long? Psc { get; set; }

        [JsonProperty("stat", NullValueHandling = NullValueHandling.Ignore)]
        public string Stat { get; set; }

        [JsonProperty("stat@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string StatRef { get; set; }

        [JsonProperty("stat@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string StatShowAs { get; set; }
    }
}