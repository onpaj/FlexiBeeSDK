using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class VazebniDoklad
    {
        [JsonProperty("idVazebniDoklad", NullValueHandling = NullValueHandling.Ignore)]
        public long? IdVazebniDoklad { get; set; }

        [JsonProperty("typVazbyK", NullValueHandling = NullValueHandling.Ignore)]
        public string TypVazbyK { get; set; }

        [JsonProperty("storno", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Storno { get; set; }

        [JsonProperty("idDokl", NullValueHandling = NullValueHandling.Ignore)]
        public long? IdDokl { get; set; }

        [JsonProperty("idDokl@evidencePath", NullValueHandling = NullValueHandling.Ignore)]
        public string IdDoklEvidencePath { get; set; }

        [JsonProperty("modul", NullValueHandling = NullValueHandling.Ignore)]
        public string Modul { get; set; }

        [JsonProperty("modulK", NullValueHandling = NullValueHandling.Ignore)]
        public string ModulK { get; set; }

        [JsonProperty("modulK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string ModulKShowAs { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Kod { get; set; }

        [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
        public string VarSym { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DatVyst { get; set; }

        [JsonProperty("datUcto", NullValueHandling = NullValueHandling.Ignore)]
        public string DatUcto { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkem { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkemMen { get; set; }

        [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
        public string Popis { get; set; }

        [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
        public string Poznam { get; set; }

        [JsonProperty("uroven", NullValueHandling = NullValueHandling.Ignore)]
        public long? Uroven { get; set; }

        [JsonProperty("stavK", NullValueHandling = NullValueHandling.Ignore)]
        public string StavK { get; set; }

        [JsonProperty("stavK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string StavKShowAs { get; set; }

        [JsonProperty("typDokl", NullValueHandling = NullValueHandling.Ignore)]
        public string TypDokl { get; set; }

        [JsonProperty("typDokl@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypDoklShowAs { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Mena { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaShowAs { get; set; }

        [JsonProperty("uzivatel", NullValueHandling = NullValueHandling.Ignore)]
        public string Uzivatel { get; set; }

        [JsonProperty("uzivatel@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string UzivatelRef { get; set; }

        [JsonProperty("uzivatel@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string UzivatelShowAs { get; set; }

        [JsonProperty("nazFirmy", NullValueHandling = NullValueHandling.Ignore)]
        public string NazFirmy { get; set; }

        [JsonProperty("mesto", NullValueHandling = NullValueHandling.Ignore)]
        public string Mesto { get; set; }

        [JsonProperty("juhSum", NullValueHandling = NullValueHandling.Ignore)]
        public string JuhSum { get; set; }

        [JsonProperty("juhSumMen", NullValueHandling = NullValueHandling.Ignore)]
        public string JuhSumMen { get; set; }

        [JsonProperty("zbyvaUhradit", NullValueHandling = NullValueHandling.Ignore)]
        public string ZbyvaUhradit { get; set; }

        [JsonProperty("zbyvaUhraditMen", NullValueHandling = NullValueHandling.Ignore)]
        public string ZbyvaUhraditMen { get; set; }

        [JsonProperty("typVazbyK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypVazbyKShowAs { get; set; }
    }
}