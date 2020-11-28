using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class FakturaPrijata
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Kod { get; set; }

        [JsonProperty("stavUhrK", NullValueHandling = NullValueHandling.Ignore)]
        public string StavUhrK { get; set; }

        [JsonProperty("stavUhrK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string StavUhrKShowAs { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DatVyst { get; set; }

        [JsonProperty("datSplat", NullValueHandling = NullValueHandling.Ignore)]
        public string DatSplat { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkem { get; set; }

        [JsonProperty("sumZalohy", NullValueHandling = NullValueHandling.Ignore)]
        public string SumZalohy { get; set; }

        [JsonProperty("sumZalohyMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumZalohyMen { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkemMen { get; set; }

        [JsonProperty("zbyvaUhraditMen", NullValueHandling = NullValueHandling.Ignore)]
        public string ZbyvaUhraditMen { get; set; }

        [JsonProperty("zbyvaUhradit", NullValueHandling = NullValueHandling.Ignore)]
        public string ZbyvaUhradit { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Mena { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaShowAs { get; set; }

        [JsonProperty("firma", NullValueHandling = NullValueHandling.Ignore)]
        public string Firma { get; set; }

        [JsonProperty("firma@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string FirmaRef { get; set; }

        [JsonProperty("firma@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string FirmaShowAs { get; set; }

        [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
        public string Popis { get; set; }

        [JsonProperty("polozkyDokladu", NullValueHandling = NullValueHandling.Ignore)]
        public List<PolozkaDokladu> PolozkyDokladu { get; set; }
    }
}