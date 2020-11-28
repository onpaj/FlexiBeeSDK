using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class SkladovyPohyb
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Kod { get; set; }

        [JsonProperty("zamekK", NullValueHandling = NullValueHandling.Ignore)]
        public string ZamekK { get; set; }

        [JsonProperty("zamekK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string ZamekKShowAs { get; set; }

        [JsonProperty("typPohybuK", NullValueHandling = NullValueHandling.Ignore)]
        public string TypPohybuK { get; set; }

        [JsonProperty("typPohybuK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypPohybuKShowAs { get; set; }

        [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
        public string VarSym { get; set; }

        [JsonProperty("cisObj", NullValueHandling = NullValueHandling.Ignore)]
        public string CisObj { get; set; }

        [JsonProperty("cisDodak", NullValueHandling = NullValueHandling.Ignore)]
        public string CisDodak { get; set; }

        [JsonProperty("doprava", NullValueHandling = NullValueHandling.Ignore)]
        public string Doprava { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DatVyst { get; set; }

        [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
        public string Popis { get; set; }

        [JsonProperty("poznam", NullValueHandling = NullValueHandling.Ignore)]
        public string Poznam { get; set; }

        [JsonProperty("uvodTxt", NullValueHandling = NullValueHandling.Ignore)]
        public string UvodTxt { get; set; }

        [JsonProperty("zavTxt", NullValueHandling = NullValueHandling.Ignore)]
        public string ZavTxt { get; set; }

        [JsonProperty("sumOsv", NullValueHandling = NullValueHandling.Ignore)]
        public string SumOsv { get; set; }

        [JsonProperty("sumZklCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public string SumZklCelkem { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkem { get; set; }

        [JsonProperty("sumOsvMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumOsvMen { get; set; }

        [JsonProperty("sumZklCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumZklCelkemMen { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkemMen { get; set; }

        [JsonProperty("kurz", NullValueHandling = NullValueHandling.Ignore)]
        public string Kurz { get; set; }

        [JsonProperty("kurzMnozstvi", NullValueHandling = NullValueHandling.Ignore)]
        public string KurzMnozstvi { get; set; }

        [JsonProperty("nazFirmy", NullValueHandling = NullValueHandling.Ignore)]
        public string NazFirmy { get; set; }

        [JsonProperty("faNazev2", NullValueHandling = NullValueHandling.Ignore)]
        public string FaNazev2 { get; set; }

        [JsonProperty("ulice", NullValueHandling = NullValueHandling.Ignore)]
        public string Ulice { get; set; }

        [JsonProperty("mesto", NullValueHandling = NullValueHandling.Ignore)]
        public string Mesto { get; set; }

        [JsonProperty("psc", NullValueHandling = NullValueHandling.Ignore)]
        public string Psc { get; set; }

        [JsonProperty("eanKod", NullValueHandling = NullValueHandling.Ignore)]
        public string EanKod { get; set; }

        [JsonProperty("ic", NullValueHandling = NullValueHandling.Ignore)]
        public string Ic { get; set; }

        [JsonProperty("dic", NullValueHandling = NullValueHandling.Ignore)]
        public string Dic { get; set; }

        [JsonProperty("pocetPriloh", NullValueHandling = NullValueHandling.Ignore)]
        public long? PocetPriloh { get; set; }

        [JsonProperty("postovniShodna", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PostovniShodna { get; set; }

        [JsonProperty("faNazev", NullValueHandling = NullValueHandling.Ignore)]
        public string FaNazev { get; set; }

        [JsonProperty("faUlice", NullValueHandling = NullValueHandling.Ignore)]
        public string FaUlice { get; set; }

        [JsonProperty("faMesto", NullValueHandling = NullValueHandling.Ignore)]
        public string FaMesto { get; set; }

        [JsonProperty("faPsc", NullValueHandling = NullValueHandling.Ignore)]
        public string FaPsc { get; set; }

        [JsonProperty("faStat", NullValueHandling = NullValueHandling.Ignore)]
        public string FaStat { get; set; }

        [JsonProperty("faEanKod", NullValueHandling = NullValueHandling.Ignore)]
        public string FaEanKod { get; set; }

        [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
        public bool? BezPolozek { get; set; }

        [JsonProperty("ucetni", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Ucetni { get; set; }

        [JsonProperty("zuctovano", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Zuctovano { get; set; }

        [JsonProperty("datUcto", NullValueHandling = NullValueHandling.Ignore)]
        public string DatUcto { get; set; }

        [JsonProperty("storno", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Storno { get; set; }

        [JsonProperty("stitky", NullValueHandling = NullValueHandling.Ignore)]
        public string Stitky { get; set; }

        [JsonProperty("hromFakt", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HromFakt { get; set; }

        [JsonProperty("typDokl", NullValueHandling = NullValueHandling.Ignore)]
        public string TypDokl { get; set; }

        [JsonProperty("typDokl@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string TypDoklRef { get; set; }

        [JsonProperty("typDokl@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypDoklShowAs { get; set; }

        [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
        public string Sklad { get; set; }

        [JsonProperty("sklad@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string SkladRef { get; set; }

        [JsonProperty("sklad@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string SkladShowAs { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Mena { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaShowAs { get; set; }

        [JsonProperty("firma", NullValueHandling = NullValueHandling.Ignore)]
        public string Firma { get; set; }

        [JsonProperty("stat", NullValueHandling = NullValueHandling.Ignore)]
        public string Stat { get; set; }

        [JsonProperty("mistUrc", NullValueHandling = NullValueHandling.Ignore)]
        public string MistUrc { get; set; }

        [JsonProperty("typUcOp", NullValueHandling = NullValueHandling.Ignore)]
        public string TypUcOp { get; set; }

        [JsonProperty("typUcOp@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string TypUcOpRef { get; set; }

        [JsonProperty("typUcOp@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypUcOpShowAs { get; set; }

        [JsonProperty("primUcet", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimUcet { get; set; }

        [JsonProperty("primUcet@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimUcetRef { get; set; }

        [JsonProperty("primUcet@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimUcetShowAs { get; set; }

        [JsonProperty("protiUcet", NullValueHandling = NullValueHandling.Ignore)]
        public string ProtiUcet { get; set; }

        [JsonProperty("protiUcet@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string ProtiUcetRef { get; set; }

        [JsonProperty("protiUcet@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string ProtiUcetShowAs { get; set; }

        [JsonProperty("stredisko", NullValueHandling = NullValueHandling.Ignore)]
        public string Stredisko { get; set; }

        [JsonProperty("stredisko@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string StrediskoRef { get; set; }

        [JsonProperty("stredisko@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string StrediskoShowAs { get; set; }

        [JsonProperty("cinnost", NullValueHandling = NullValueHandling.Ignore)]
        public string Cinnost { get; set; }

        [JsonProperty("zakazka", NullValueHandling = NullValueHandling.Ignore)]
        public string Zakazka { get; set; }

        [JsonProperty("statOdesl", NullValueHandling = NullValueHandling.Ignore)]
        public string StatOdesl { get; set; }

        [JsonProperty("statUrc", NullValueHandling = NullValueHandling.Ignore)]
        public string StatUrc { get; set; }

        [JsonProperty("statPuvod", NullValueHandling = NullValueHandling.Ignore)]
        public string StatPuvod { get; set; }

        [JsonProperty("dodPodm", NullValueHandling = NullValueHandling.Ignore)]
        public string DodPodm { get; set; }

        [JsonProperty("obchTrans", NullValueHandling = NullValueHandling.Ignore)]
        public string ObchTrans { get; set; }

        [JsonProperty("druhDopr", NullValueHandling = NullValueHandling.Ignore)]
        public string DruhDopr { get; set; }

        [JsonProperty("zvlPoh", NullValueHandling = NullValueHandling.Ignore)]
        public string ZvlPoh { get; set; }

        [JsonProperty("krajUrc", NullValueHandling = NullValueHandling.Ignore)]
        public string KrajUrc { get; set; }

        [JsonProperty("uzivatel", NullValueHandling = NullValueHandling.Ignore)]
        public string Uzivatel { get; set; }

        [JsonProperty("uzivatel@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string UzivatelRef { get; set; }

        [JsonProperty("uzivatel@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string UzivatelShowAs { get; set; }

        [JsonProperty("zodpOsoba", NullValueHandling = NullValueHandling.Ignore)]
        public string ZodpOsoba { get; set; }

        [JsonProperty("zodpOsoba@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string ZodpOsobaRef { get; set; }

        [JsonProperty("zodpOsoba@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string ZodpOsobaShowAs { get; set; }

        [JsonProperty("kontaktOsoba", NullValueHandling = NullValueHandling.Ignore)]
        public string KontaktOsoba { get; set; }

        [JsonProperty("kontaktJmeno", NullValueHandling = NullValueHandling.Ignore)]
        public string KontaktJmeno { get; set; }

        [JsonProperty("kontaktEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string KontaktEmail { get; set; }

        [JsonProperty("kontaktTel", NullValueHandling = NullValueHandling.Ignore)]
        public string KontaktTel { get; set; }

        [JsonProperty("rada", NullValueHandling = NullValueHandling.Ignore)]
        public string Rada { get; set; }

        [JsonProperty("rada@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string RadaShowAs { get; set; }

        [JsonProperty("rada@if-not-found", NullValueHandling = NullValueHandling.Ignore)]
        public string RadaIfNotFound { get; set; }

        [JsonProperty("rada@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string RadaRef { get; set; }

        [JsonProperty("zdrojProFak", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ZdrojProFak { get; set; }

        [JsonProperty("stavSkladK", NullValueHandling = NullValueHandling.Ignore)]
        public string StavSkladK { get; set; }

        [JsonProperty("stavSkladK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string StavSkladKShowAs { get; set; }

        [JsonProperty("typPohybuSkladK", NullValueHandling = NullValueHandling.Ignore)]
        public string TypPohybuSkladK { get; set; }

        [JsonProperty("typPohybuSkladK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string TypPohybuSkladKShowAs { get; set; }

        [JsonProperty("formaDopravy", NullValueHandling = NullValueHandling.Ignore)]
        public string FormaDopravy { get; set; }

        [JsonProperty("uuid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uuid { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("vyloucitSaldo", NullValueHandling = NullValueHandling.Ignore)]
        public bool? VyloucitSaldo { get; set; }

        [JsonProperty("skladCil", NullValueHandling = NullValueHandling.Ignore)]
        public string SkladCil { get; set; }

        [JsonProperty("inventura", NullValueHandling = NullValueHandling.Ignore)]
        public string Inventura { get; set; }

        [JsonProperty("polozkyDokladu", NullValueHandling = NullValueHandling.Ignore)]
        public List<PolozkaSkladovehoDokladu> PolozkyDokladu { get; set; }
    }
}