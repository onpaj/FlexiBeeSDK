using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class PolozkaFakturyVydane
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Kod { get; set; }
        [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
        public string Nazev { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DatVyst { get; set; }

        [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
        public string Cenik { get; set; }

        [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
        public string Sklad { get; set; }

        [JsonProperty("mnozMj", NullValueHandling = NullValueHandling.Ignore)]
        public string MnozMj { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumCelkem { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumCelkemMen { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Mena { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaShowAs { get; set; }
        
        [JsonProperty("mj", NullValueHandling = NullValueHandling.Ignore)]
        public string Mj { get; set; }
        [JsonProperty("cenaMj", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CenaMj { get; set; }
        [JsonProperty("sumZklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumZklMen { get; set; }
        [JsonProperty("sumZkl", NullValueHandling = NullValueHandling.Ignore)] 
        public decimal? SumZkl { get; set; }
        [JsonProperty("typSzbDphK", NullValueHandling = NullValueHandling.Ignore)]
        public string TypSzbDphK { get; set; }
        [JsonProperty("typCenyDphK", NullValueHandling = NullValueHandling.Ignore)] 
        public string TypCenyDphK { get; set; }
    }
}