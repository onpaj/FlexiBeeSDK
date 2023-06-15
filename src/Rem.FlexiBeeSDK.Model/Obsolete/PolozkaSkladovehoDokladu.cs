using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    [Obsolete]
    public partial class PolozkaSkladovehoDokladu
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("ucetni", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Ucetni { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Kod { get; set; }

        [JsonProperty("eanKod", NullValueHandling = NullValueHandling.Ignore)]
        public string EanKod { get; set; }

        [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
        public string Nazev { get; set; }

        [JsonProperty("nazevA", NullValueHandling = NullValueHandling.Ignore)]
        public string NazevA { get; set; }

        [JsonProperty("nazevB", NullValueHandling = NullValueHandling.Ignore)]
        public string NazevB { get; set; }

        [JsonProperty("nazevC", NullValueHandling = NullValueHandling.Ignore)]
        public string NazevC { get; set; }

        [JsonProperty("cisRad", NullValueHandling = NullValueHandling.Ignore)]
        public long? CisRad { get; set; }

        [JsonProperty("typPolozkyK", NullValueHandling = NullValueHandling.Ignore)]
        public string TypPolozkyK { get; set; }

        [JsonProperty("typPolozkyK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypPolozkyKShowAs { get; set; }

        [JsonProperty("baleniId", NullValueHandling = NullValueHandling.Ignore)]
        public long? BaleniId { get; set; }

        [JsonProperty("mnozBaleni", NullValueHandling = NullValueHandling.Ignore)]
        public string MnozBaleni { get; set; }

        [JsonProperty("mnozMj", NullValueHandling = NullValueHandling.Ignore)]
        public string MnozMj { get; set; }

        [JsonProperty("mnozMjPrijem", NullValueHandling = NullValueHandling.Ignore)]
        public string MnozMjPrijem { get; set; }

        [JsonProperty("mnozMjVydej", NullValueHandling = NullValueHandling.Ignore)]
        public string MnozMjVydej { get; set; }

        [JsonProperty("mnozMjPlan", NullValueHandling = NullValueHandling.Ignore)]
        public string MnozMjPlan { get; set; }

        [JsonProperty("typCenyDphK", NullValueHandling = NullValueHandling.Ignore)]
        public string TypCenyDphK { get; set; }

        [JsonProperty("typCenyDphK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypCenyDphKShowAs { get; set; }

        [JsonProperty("typSzbDphK", NullValueHandling = NullValueHandling.Ignore)]
        public string TypSzbDphK { get; set; }

        [JsonProperty("cenaMj", NullValueHandling = NullValueHandling.Ignore)]
        public string CenaMj { get; set; }

        [JsonProperty("sumZkl", NullValueHandling = NullValueHandling.Ignore)]
        public string SumZkl { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkem { get; set; }

        [JsonProperty("sumZklMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumZklMen { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkemMen { get; set; }

        [JsonProperty("objem", NullValueHandling = NullValueHandling.Ignore)]
        public string Objem { get; set; }

        [JsonProperty("cenJednotka", NullValueHandling = NullValueHandling.Ignore)]
        public string CenJednotka { get; set; }

        [JsonProperty("cenaMjProdej", NullValueHandling = NullValueHandling.Ignore)]
        public string CenaMjProdej { get; set; }

        [JsonProperty("cenaMjCenikTuz", NullValueHandling = NullValueHandling.Ignore)]
        public string CenaMjCenikTuz { get; set; }

        [JsonProperty("sarze", NullValueHandling = NullValueHandling.Ignore)]
        public string Sarze { get; set; }

        [JsonProperty("expirace", NullValueHandling = NullValueHandling.Ignore)]
        public string Expirace { get; set; }

        [JsonProperty("datTrvan", NullValueHandling = NullValueHandling.Ignore)]
        public string DatTrvan { get; set; }

        [JsonProperty("datVyroby", NullValueHandling = NullValueHandling.Ignore)]
        public string DatVyroby { get; set; }

        [JsonProperty("autogen", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Autogen { get; set; }

        [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
        public string Poznam { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DatVyst { get; set; }

        [JsonProperty("kopZklMdUcet", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopZklMdUcet { get; set; }

        [JsonProperty("kopZklDalUcet", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopZklDalUcet { get; set; }

        [JsonProperty("kopTypUcOp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopTypUcOp { get; set; }

        [JsonProperty("kopZakazku", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopZakazku { get; set; }

        [JsonProperty("kopStred", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopStred { get; set; }

        [JsonProperty("kopCinnost", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopCinnost { get; set; }

        [JsonProperty("kopKlice", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopKlice { get; set; }

        [JsonProperty("kopDatUcto", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KopDatUcto { get; set; }

        [JsonProperty("datUcto", NullValueHandling = NullValueHandling.Ignore)]
        public string DatUcto { get; set; }

        [JsonProperty("storno", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Storno { get; set; }

        [JsonProperty("stornoPol", NullValueHandling = NullValueHandling.Ignore)]
        public bool? StornoPol { get; set; }

        [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
        public string Sklad { get; set; }

        [JsonProperty("sklad@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string SkladRef { get; set; }

        [JsonProperty("sklad@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string SkladShowAs { get; set; }

        [JsonProperty("stredisko", NullValueHandling = NullValueHandling.Ignore)]
        public string Stredisko { get; set; }

        [JsonProperty("stredisko@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string StrediskoRef { get; set; }

        [JsonProperty("stredisko@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string StrediskoShowAs { get; set; }

        [JsonProperty("cinnost", NullValueHandling = NullValueHandling.Ignore)]
        public string Cinnost { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Mena { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaShowAs { get; set; }

        [JsonProperty("typUcOp", NullValueHandling = NullValueHandling.Ignore)]
        public string TypUcOp { get; set; }

        [JsonProperty("typUcOp@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string TypUcOpRef { get; set; }

        [JsonProperty("typUcOp@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypUcOpShowAs { get; set; }

        [JsonProperty("zklMdUcet", NullValueHandling = NullValueHandling.Ignore)]
        public string ZklMdUcet { get; set; }

        [JsonProperty("zklMdUcet@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string ZklMdUcetRef { get; set; }

        [JsonProperty("zklMdUcet@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string ZklMdUcetShowAs { get; set; }

        [JsonProperty("zklDalUcet", NullValueHandling = NullValueHandling.Ignore)]
        public string ZklDalUcet { get; set; }

        [JsonProperty("zklDalUcet@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string ZklDalUcetRef { get; set; }

        [JsonProperty("zklDalUcet@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string ZklDalUcetShowAs { get; set; }

        [JsonProperty("cenHlad", NullValueHandling = NullValueHandling.Ignore)]
        public string CenHlad { get; set; }

        [JsonProperty("zakazka", NullValueHandling = NullValueHandling.Ignore)]
        public string Zakazka { get; set; }

        [JsonProperty("dodavatel", NullValueHandling = NullValueHandling.Ignore)]
        public string Dodavatel { get; set; }

        [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
        public string Cenik { get; set; }

        [JsonProperty("cenik@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string CenikRef { get; set; }

        [JsonProperty("cenik@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string CenikShowAs { get; set; }

        [JsonProperty("mj", NullValueHandling = NullValueHandling.Ignore)]
        public string Mj { get; set; }

        [JsonProperty("mj@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string MjRef { get; set; }

        [JsonProperty("mj@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string MjShowAs { get; set; }

        [JsonProperty("mjObjem", NullValueHandling = NullValueHandling.Ignore)]
        public string MjObjem { get; set; }

        [JsonProperty("doklSklad", NullValueHandling = NullValueHandling.Ignore)]
        public string DoklSklad { get; set; }

        [JsonProperty("doklSklad@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string DoklSkladRef { get; set; }

        [JsonProperty("doklSklad@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string DoklSkladShowAs { get; set; }

        [JsonProperty("skladovaKarta", NullValueHandling = NullValueHandling.Ignore)]
        public long? SkladovaKarta { get; set; }

        [JsonProperty("skladovaKarta@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string SkladovaKartaRef { get; set; }

        [JsonProperty("skladovaKarta@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string SkladovaKartaShowAs { get; set; }

        [JsonProperty("vyrobniCislaOk", NullValueHandling = NullValueHandling.Ignore)]
        public bool? VyrobniCislaOk { get; set; }

        [JsonProperty("prevodka", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Prevodka { get; set; }

        [JsonProperty("zdrojProFak", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ZdrojProFak { get; set; }

        [JsonProperty("cenaMjPoriz", NullValueHandling = NullValueHandling.Ignore)]
        public string CenaMjPoriz { get; set; }

        [JsonProperty("cenaMjNakl", NullValueHandling = NullValueHandling.Ignore)]
        public string CenaMjNakl { get; set; }

        [JsonProperty("cenaMjNeskl", NullValueHandling = NullValueHandling.Ignore)]
        public string CenaMjNeskl { get; set; }

        [JsonProperty("cenaMjSkl", NullValueHandling = NullValueHandling.Ignore)]
        public string CenaMjSkl { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("cenyRucne", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CenyRucne { get; set; }

        [JsonProperty("stitky", NullValueHandling = NullValueHandling.Ignore)]
        public string Stitky { get; set; }

        [JsonProperty("vyrobniCislaPrijata", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> VyrobniCislaPrijata { get; set; }

        [JsonProperty("vyrobniCislaVydana", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> VyrobniCislaVydana { get; set; }
    }
}