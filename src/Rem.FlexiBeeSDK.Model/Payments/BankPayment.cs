using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Payments;

public class BankPayment
{
        [JsonProperty("id")] public int Id { get; set; }

        // [JsonProperty("id@editable")]
        // public bool IdEditable { get; set; }
        //
        // [JsonProperty("duzpUcto")]
        // public string DuzpUcto { get; set; }
        //
        // [JsonProperty("duzpUcto@visible")]
        // public bool DuzpUctoVisible { get; set; }
        //
        // [JsonProperty("duzpUcto@enabled")]
        // public bool DuzpUctoEnabled { get; set; }
        //
        // [JsonProperty("kurz")]
        // public double Kurz { get; set; }
        //
        // [JsonProperty("kurz@enabled")]
        // public bool KurzEnabled { get; set; }
        //
        // [JsonProperty("uzpTuzemsko")]
        // public bool UzpTuzemsko { get; set; }
        //
        // [JsonProperty("uzpTuzemsko@visible")]
        // public bool UzpTuzemskoVisible { get; set; }
        //
        // [JsonProperty("cisObj")]
        // public string CisObj { get; set; }
        //
        // [JsonProperty("stavUzivK")]
        // public string StavUzivK { get; set; }
        //
        // [JsonProperty("stavUzivK@possibleValues")]
        // public string StavUzivKPossibleValues { get; set; }
        //
        // [JsonProperty("stavUzivK@showAs")]
        // public string StavUzivKShowAs { get; set; }
        //
        // [JsonProperty("dphSniz2Ucet")]
        // public string DphSniz2Ucet { get; set; }
        //
        // [JsonProperty("dphSniz2Ucet@evidencePath")]
        // public string DphSniz2UcetEvidencePath { get; set; }
        //
        // [JsonProperty("sumZklSnizMen")]
        // public double SumZklSnizMen { get; set; }
        //
        // [JsonProperty("sumZklSnizMen@label")]
        // public string SumZklSnizMenLabel { get; set; }
        //
        // [JsonProperty("sumZklSnizMen@enabled")]
        // public bool SumZklSnizMenEnabled { get; set; }
        //
        // [JsonProperty("szbDphZakl")]
        // public double SzbDphZakl { get; set; }
        //
        // [JsonProperty("storno")]
        // public bool Storno { get; set; }
        //
        // [JsonProperty("storno@editable")]
        // public bool StornoEditable { get; set; }
        //
        // [JsonProperty("szbDphSniz")]
        // public double SzbDphSniz { get; set; }
        //
        // [JsonProperty("ic")]
        // public string Ic { get; set; }
        //
        // [JsonProperty("dphSnizUcet")]
        // public string DphSnizUcet { get; set; }
        //
        // [JsonProperty("dphSnizUcet@evidencePath")]
        // public string DphSnizUcetEvidencePath { get; set; }
        //
        // [JsonProperty("zakazka@evidencePath")]
        // public string ZakazkaEvidencePath { get; set; }
        //
        // [JsonProperty("zakazka")]
        // public List<object> Zakazka { get; set; }
        //
        // [JsonProperty("kontaktEmail")]
        // public string KontaktEmail { get; set; }
        //
        // [JsonProperty("typUcOp@evidencePath")]
        // public string TypUcOpEvidencePath { get; set; }
        //
        // [JsonProperty("typUcOp@filterValues")]
        // public string TypUcOpFilterValues { get; set; }
        //
        // [JsonProperty("typUcOp")]
        // public List<object> TypUcOp { get; set; }
        //
        // [JsonProperty("sumOsv")]
        // public double SumOsv { get; set; }
        //
        // [JsonProperty("sumOsv@label")]
        // public string SumOsvLabel { get; set; }
        //
        // [JsonProperty("sumCelkSnizMen")]
        // public double SumCelkSnizMen { get; set; }
        //
        // [JsonProperty("sumCelkSnizMen@enabled")]
        // public bool SumCelkSnizMenEnabled { get; set; }
        //
        // [JsonProperty("nazFirmy")]
        // public string NazFirmy { get; set; }
        //
        // [JsonProperty("eanKod")]
        // public string EanKod { get; set; }
        //
        // [JsonProperty("firma@evidencePath")]
        // public string FirmaEvidencePath { get; set; }
        //
        // [JsonProperty("firma")]
        // public List<object> Firma { get; set; }
        //
        // [JsonProperty("sumCelkSniz2")]
        // public double SumCelkSniz2 { get; set; }
        //
        // [JsonProperty("sumZklZakl")]
        // public double SumZklZakl { get; set; }
        //
        // [JsonProperty("sumZklZakl@label")]
        // public string SumZklZaklLabel { get; set; }
        //
        // [JsonProperty("sumDphSniz")]
        // public double SumDphSniz { get; set; }
        //
        // [JsonProperty("sumCelkem")]
        // public double SumCelkem { get; set; }
        //
        // [JsonProperty("clenKonVykDph")]
        // public string ClenKonVykDph { get; set; }
        //
        // [JsonProperty("clenKonVykDph@evidencePath")]
        // public string ClenKonVykDphEvidencePath { get; set; }
        //
        // [JsonProperty("clenKonVykDph@internalId")]
        // public int ClenKonVykDphInternalId { get; set; }
        //
        // [JsonProperty("clenKonVykDph@ref")]
        // public string ClenKonVykDphRef { get; set; }
        //
        // [JsonProperty("clenKonVykDph@showAs")]
        // public string ClenKonVykDphShowAs { get; set; }
        //
        // [JsonProperty("sumDphZakl")]
        // public double SumDphZakl { get; set; }
        //
        // [JsonProperty("lastUpdate")]
        // public DateTime LastUpdate { get; set; }
        //
        // [JsonProperty("lastUpdate@visible")]
        // public bool LastUpdateVisible { get; set; }
        //
        // [JsonProperty("lastUpdate@editable")]
        // public bool LastUpdateEditable { get; set; }
        //
        // [JsonProperty("lastUpdate@enabled")]
        // public bool LastUpdateEnabled { get; set; }
        //
        // [JsonProperty("ulice")]
        // public string Ulice { get; set; }
        //
        // [JsonProperty("iban")]
        // public string Iban { get; set; }
        //
        // [JsonProperty("sumZklSniz")]
        // public double SumZklSniz { get; set; }
        //
        // [JsonProperty("sumZklSniz@label")]
        // public string SumZklSnizLabel { get; set; }
        //
        // [JsonProperty("konSym")]
        // public string KonSym { get; set; }
        //
        // [JsonProperty("konSym@evidencePath")]
        // public string KonSymEvidencePath { get; set; }
        //
        // [JsonProperty("konSym@internalId")]
        // public int KonSymInternalId { get; set; }
        //
        // [JsonProperty("konSym@ref")]
        // public string KonSymRef { get; set; }
        //
        // [JsonProperty("konSym@showAs")]
        // public string KonSymShowAs { get; set; }
        //
        // [JsonProperty("clenDph")]
        // public string ClenDph { get; set; }
        //
        // [JsonProperty("clenDph@evidencePath")]
        // public string ClenDphEvidencePath { get; set; }
        //
        // [JsonProperty("clenDph@internalId")]
        // public int ClenDphInternalId { get; set; }
        //
        // [JsonProperty("clenDph@ref")]
        // public string ClenDphRef { get; set; }
        //
        // [JsonProperty("clenDph@showAs")]
        // public string ClenDphShowAs { get; set; }
        //
        // [JsonProperty("kontaktJmeno")]
        // public string KontaktJmeno { get; set; }
        //
        // [JsonProperty("banka@evidencePath")]
        // public string BankaEvidencePath { get; set; }
        //
        // [JsonProperty("banka@internalId")]
        // public int BankaInternalId { get; set; }
        //
        // [JsonProperty("banka@ref")]
        // public string BankaRef { get; set; }
        //
        // [JsonProperty("banka@showAs")]
        // public string BankaShowAs { get; set; }
        //
        // [JsonProperty("banka")]
        // public List<Banka> Banka { get; set; }
        //
        // [JsonProperty("vyloucitSaldo")]
        // public bool VyloucitSaldo { get; set; }
        //
        // [JsonProperty("kontaktOsoba@evidencePath")]
        // public string KontaktOsobaEvidencePath { get; set; }
        //
        // [JsonProperty("kontaktOsoba")]
        // public List<object> KontaktOsoba { get; set; }
        //
        // [JsonProperty("specSym")]
        // public string SpecSym { get; set; }
        //
        // [JsonProperty("cisDosle")]
        // public string CisDosle { get; set; }
        //
        // [JsonProperty("smerKod@evidencePath")]
        // public string SmerKodEvidencePath { get; set; }
        //
        // [JsonProperty("smerKod@internalId")]
        // public int SmerKodInternalId { get; set; }
        //
        // [JsonProperty("smerKod@ref")]
        // public string SmerKodRef { get; set; }
        //
        // [JsonProperty("smerKod@showAs")]
        // public string SmerKodShowAs { get; set; }
        //
        // [JsonProperty("smerKod")]
        // public List<SmerKod> SmerKod { get; set; }
        //
        // [JsonProperty("sumZklSniz2Men")]
        // public double SumZklSniz2Men { get; set; }
        //
        // [JsonProperty("sumZklSniz2Men@label")]
        // public string SumZklSniz2MenLabel { get; set; }
        //
        // [JsonProperty("sumZklSniz2Men@enabled")]
        // public bool SumZklSniz2MenEnabled { get; set; }
        //
        // [JsonProperty("typDokl@evidencePath")]
        // public string TypDoklEvidencePath { get; set; }
        //
        // [JsonProperty("typDokl@internalId")]
        // public int TypDoklInternalId { get; set; }
        //
        // [JsonProperty("typDokl@ref")]
        // public string TypDoklRef { get; set; }
        //
        // [JsonProperty("typDokl@showAs")]
        // public string TypDoklShowAs { get; set; }
        //
        // [JsonProperty("typDokl")]
        // public List<TypDokl> TypDokl { get; set; }
        //
        // [JsonProperty("szbDphSniz2")]
        // public double SzbDphSniz2 { get; set; }
        //
        // [JsonProperty("sumZklSniz2")]
        // public double SumZklSniz2 { get; set; }
        //
        // [JsonProperty("sumZklSniz2@label")]
        // public string SumZklSniz2Label { get; set; }
        //
        // [JsonProperty("protiUcet")]
        // public string ProtiUcet { get; set; }
        //
        // [JsonProperty("protiUcet@evidencePath")]
        // public string ProtiUcetEvidencePath { get; set; }
        //
        // [JsonProperty("protiUcet@internalId")]
        // public int ProtiUcetInternalId { get; set; }
        //
        // [JsonProperty("protiUcet@ref")]
        // public string ProtiUcetRef { get; set; }
        //
        // [JsonProperty("protiUcet@showAs")]
        // public string ProtiUcetShowAs { get; set; }
        //
        // [JsonProperty("sparovano")]
        // public bool Sparovano { get; set; }
        //
        // [JsonProperty("sparovano@editable")]
        // public bool SparovanoEditable { get; set; }
        //
        // [JsonProperty("zodpOsoba@evidencePath")]
        // public string ZodpOsobaEvidencePath { get; set; }
        //
        // [JsonProperty("zodpOsoba@internalId")]
        // public int ZodpOsobaInternalId { get; set; }
        //
        // [JsonProperty("zodpOsoba@ref")]
        // public string ZodpOsobaRef { get; set; }
        //
        // [JsonProperty("zodpOsoba@showAs")]
        // public string ZodpOsobaShowAs { get; set; }
        //
        // [JsonProperty("zodpOsoba")]
        // public List<ZodpOsoba> ZodpOsoba { get; set; }
        //
        // [JsonProperty("sumDphSniz2")]
        // public double SumDphSniz2 { get; set; }
        //
        // [JsonProperty("stat@evidencePath")]
        // public string StatEvidencePath { get; set; }
        //
        // [JsonProperty("stat@internalId")]
        // public int StatInternalId { get; set; }
        //
        // [JsonProperty("stat@ref")]
        // public string StatRef { get; set; }
        //
        // [JsonProperty("stat@showAs")]
        // public string StatShowAs { get; set; }
        //
        // [JsonProperty("stat")]
        // public List<Stat> Stat { get; set; }
        //
        // [JsonProperty("kurzMnozstvi")]
        // public double KurzMnozstvi { get; set; }
        //
        // [JsonProperty("kurzMnozstvi@enabled")]
        // public bool KurzMnozstviEnabled { get; set; }
        //
        // [JsonProperty("popis")]
        // public string Popis { get; set; }
        //
        // [JsonProperty("stitky")]
        // public string Stitky { get; set; }
        //
        // [JsonProperty("buc")]
        // public string Buc { get; set; }
        //
        // [JsonProperty("mena@evidencePath")]
        // public string MenaEvidencePath { get; set; }
        //
        // [JsonProperty("mena@internalId")]
        // public int MenaInternalId { get; set; }
        //
        // [JsonProperty("mena@ref")]
        // public string MenaRef { get; set; }
        //
        // [JsonProperty("mena@showAs")]
        // public string MenaShowAs { get; set; }
        //
        // [JsonProperty("mena")]
        // public List<Mena> Mena { get; set; }
        //
        // [JsonProperty("dphZaklUcet")]
        // public string DphZaklUcet { get; set; }
        //
        // [JsonProperty("dphZaklUcet@evidencePath")]
        // public string DphZaklUcetEvidencePath { get; set; }
        //
        // [JsonProperty("sumDphZaklMen")]
        // public double SumDphZaklMen { get; set; }
        //
        // [JsonProperty("sumDphZaklMen@enabled")]
        // public bool SumDphZaklMenEnabled { get; set; }
        //
        // [JsonProperty("dic")]
        // public string Dic { get; set; }
        //
        // [JsonProperty("kod")]
        // public string Kod { get; set; }
        //
        // [JsonProperty("statDph")]
        // public string StatDph { get; set; }
        //
        // [JsonProperty("statDph@evidencePath")]
        // public string StatDphEvidencePath { get; set; }
        //
        // [JsonProperty("statDph@internalId")]
        // public int StatDphInternalId { get; set; }
        //
        // [JsonProperty("statDph@ref")]
        // public string StatDphRef { get; set; }
        //
        // [JsonProperty("statDph@showAs")]
        // public string StatDphShowAs { get; set; }
        //
        // [JsonProperty("sumCelkZaklMen")]
        // public double SumCelkZaklMen { get; set; }
        //
        // [JsonProperty("sumCelkZaklMen@enabled")]
        // public bool SumCelkZaklMenEnabled { get; set; }
        //
        // [JsonProperty("sumOsvMen")]
        // public double SumOsvMen { get; set; }
        //
        // [JsonProperty("sumOsvMen@label")]
        // public string SumOsvMenLabel { get; set; }
        //
        // [JsonProperty("sumOsvMen@enabled")]
        // public bool SumOsvMenEnabled { get; set; }
        //
        // [JsonProperty("duzpPuv")]
        // public string DuzpPuv { get; set; }
        //
        // [JsonProperty("source")]
        // public string Source { get; set; }
        //
        // [JsonProperty("typPohybuK")]
        // public string TypPohybuK { get; set; }
        //
        // [JsonProperty("typPohybuK@possibleValues")]
        // public string TypPohybuKPossibleValues { get; set; }
        //
        // [JsonProperty("typPohybuK@showAs")]
        // public string TypPohybuKShowAs { get; set; }
        //
        // [JsonProperty("sumCelkSniz2Men")]
        // public double SumCelkSniz2Men { get; set; }
        //
        // [JsonProperty("sumCelkSniz2Men@enabled")]
        // public bool SumCelkSniz2MenEnabled { get; set; }
        //
        // [JsonProperty("sumCelkemMen")]
        // public double SumCelkemMen { get; set; }
        //
        // [JsonProperty("sumCelkemMen@enabled")]
        // public bool SumCelkemMenEnabled { get; set; }
        //
        // [JsonProperty("vypisCisDokl")]
        // public string VypisCisDokl { get; set; }
        //
        // [JsonProperty("datVyst")]
        // public string DatVyst { get; set; }
        //
        // [JsonProperty("poznam")]
        // public string Poznam { get; set; }
        //
        // [JsonProperty("sumDphSnizMen")]
        // public double SumDphSnizMen { get; set; }
        //
        // [JsonProperty("sumDphSnizMen@enabled")]
        // public bool SumDphSnizMenEnabled { get; set; }
        //
        // [JsonProperty("zapocet")]
        // public bool Zapocet { get; set; }
        //
        // [JsonProperty("sumCelkSniz")]
        // public double SumCelkSniz { get; set; }
        //
        // [JsonProperty("sumCelkZakl")]
        // public double SumCelkZakl { get; set; }
        //
        // [JsonProperty("mesto")]
        // public string Mesto { get; set; }
        //
        // [JsonProperty("datUcto")]
        // public string DatUcto { get; set; }
        //
        // [JsonProperty("psc")]
        // public string Psc { get; set; }
        //
        // [JsonProperty("zamekK")]
        // public string ZamekK { get; set; }
        //
        // [JsonProperty("zamekK@possibleValues")]
        // public string ZamekKPossibleValues { get; set; }
        //
        // [JsonProperty("zamekK@showAs")]
        // public string ZamekKShowAs { get; set; }
        //
        // [JsonProperty("zamekK@editable")]
        // public bool ZamekKEditable { get; set; }
        //
        // [JsonProperty("stredisko@evidencePath")]
        // public string StrediskoEvidencePath { get; set; }
        //
        // [JsonProperty("stredisko@internalId")]
        // public int StrediskoInternalId { get; set; }
        //
        // [JsonProperty("stredisko@ref")]
        // public string StrediskoRef { get; set; }
        //
        // [JsonProperty("stredisko@showAs")]
        // public string StrediskoShowAs { get; set; }
        //
        // [JsonProperty("jakUhrK")]
        // public string JakUhrK { get; set; }
        //
        // [JsonProperty("jakUhrK@possibleValues")]
        // public string JakUhrKPossibleValues { get; set; }
        //
        // [JsonProperty("varSym")]
        // public string VarSym { get; set; }
        //
        // [JsonProperty("kontaktTel")]
        // public string KontaktTel { get; set; }
        //
        // [JsonProperty("banSpojDod@evidencePath")]
        // public string BanSpojDodEvidencePath { get; set; }
        //
        // [JsonProperty("banSpojDod@filterValues")]
        // public string BanSpojDodFilterValues { get; set; }
        //
        // [JsonProperty("banSpojDod@if-not-found")]
        // public string BanSpojDodIfNotFound { get; set; }
        //
        // [JsonProperty("banSpojDod")]
        // public List<object> BanSpojDod { get; set; }
        //
        // [JsonProperty("uzivatel")]
        // public string Uzivatel { get; set; }
        //
        // [JsonProperty("uzivatel@evidencePath")]
        // public string UzivatelEvidencePath { get; set; }
        //
        // [JsonProperty("uzivatel@internalId")]
        // public int UzivatelInternalId { get; set; }
        //
        // [JsonProperty("uzivatel@ref")]
        // public string UzivatelRef { get; set; }
        //
        // [JsonProperty("uzivatel@showAs")]
        // public string UzivatelShowAs { get; set; }
        //
        // [JsonProperty("uzivatel@editable")]
        // public bool UzivatelEditable { get; set; }
        //
        // [JsonProperty("rada")]
        // public string Rada { get; set; }
        //
        // [JsonProperty("rada@evidencePath")]
        // public string RadaEvidencePath { get; set; }
        //
        // [JsonProperty("rada@internalId")]
        // public int RadaInternalId { get; set; }
        //
        // [JsonProperty("rada@ref")]
        // public string RadaRef { get; set; }
        //
        // [JsonProperty("rada@showAs")]
        // public string RadaShowAs { get; set; }
        //
        // [JsonProperty("rada@if-not-found")]
        // public string RadaIfNotFound { get; set; }
        //
        // [JsonProperty("sumZklZaklMen")]
        // public double SumZklZaklMen { get; set; }
        //
        // [JsonProperty("sumZklZaklMen@label")]
        // public string SumZklZaklMenLabel { get; set; }
        //
        // [JsonProperty("sumZklZaklMen@enabled")]
        // public bool SumZklZaklMenEnabled { get; set; }
        //
        // [JsonProperty("primUcet")]
        // public string PrimUcet { get; set; }
        //
        // [JsonProperty("primUcet@evidencePath")]
        // public string PrimUcetEvidencePath { get; set; }
        //
        // [JsonProperty("primUcet@internalId")]
        // public int PrimUcetInternalId { get; set; }
        //
        // [JsonProperty("primUcet@ref")]
        // public string PrimUcetRef { get; set; }
        //
        // [JsonProperty("primUcet@showAs")]
        // public string PrimUcetShowAs { get; set; }
        //
        // [JsonProperty("cinnost")]
        // public string Cinnost { get; set; }
        //
        // [JsonProperty("cinnost@evidencePath")]
        // public string CinnostEvidencePath { get; set; }
        //
        // [JsonProperty("cisSouhrnne")]
        // public string CisSouhrnne { get; set; }
        //
        // [JsonProperty("cisSouhrnne@editable")]
        // public bool CisSouhrnneEditable { get; set; }
        //
        // [JsonProperty("zuctovano")]
        // public bool Zuctovano { get; set; }
        //
        // [JsonProperty("zuctovano@editable")]
        // public bool ZuctovanoEditable { get; set; }
        //
        // [JsonProperty("bic")]
        // public string Bic { get; set; }
        //
        // [JsonProperty("sumDphSniz2Men")]
        // public double SumDphSniz2Men { get; set; }
        //
        // [JsonProperty("sumDphSniz2Men@enabled")]
        // public bool SumDphSniz2MenEnabled { get; set; }
        //
        // [JsonProperty("prilohy")]
        // public List<object> Prilohy { get; set; }
        //
        // [JsonProperty("vazby")]
        // public List<object> Vazby { get; set; }
        //
        // [JsonProperty("smerKod@editable")]
        // public bool SmerKodEditable { get; set; }
}