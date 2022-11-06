using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class FakturaPrijata
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

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
        public decimal? SumCelkem { get; set; }

        [JsonProperty("sumZalohy", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumZalohy { get; set; }

        [JsonProperty("sumZalohyMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumZalohyMen { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumCelkemMen { get; set; }

        [JsonProperty("zbyvaUhraditMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ZbyvaUhraditMen { get; set; }

        [JsonProperty("zbyvaUhradit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ZbyvaUhradit { get; set; }

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
        public List<PolozkaFakturyPrijate> PolozkyDokladu { get; set; }

        [JsonProperty("duzpPuv", NullValueHandling = NullValueHandling.Ignore)]
        public string DuzpPuv { get; set; }

        [JsonProperty("duzpUcto", NullValueHandling = NullValueHandling.Ignore)]
        public string DuzpUcto { get; set; }

        [JsonProperty("nazFirmy", NullValueHandling = NullValueHandling.Ignore)]
        public string NazFirmy { get; set; }
        [JsonProperty("ulice", NullValueHandling = NullValueHandling.Ignore)]
        public string Ulice { get; set; }
        [JsonProperty("mesto", NullValueHandling = NullValueHandling.Ignore)]
        public string Mesto { get; set; }
        [JsonProperty("stat", NullValueHandling = NullValueHandling.Ignore)]
        public string Stat { get; set; }
        [JsonProperty("ic", NullValueHandling = NullValueHandling.Ignore)]
        public string Ic { get; set; }
        [JsonProperty("dic", NullValueHandling = NullValueHandling.Ignore)]
        public string Dic { get; set; }
        [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
        public string VarSym { get; set; }
        [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
        public bool BezPolozek { get; set; }
        [JsonProperty("typDokl", NullValueHandling = NullValueHandling.Ignore)]
        public string TypDokl { get; set; }
        
        [JsonProperty("cisDosle", NullValueHandling = NullValueHandling.Ignore)]
        public string CisDosle { get; set; }
        [JsonProperty("psc", NullValueHandling = NullValueHandling.Ignore)]
        public string Psc { get; set; }


        [JsonProperty("sumCelkZakl", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumCelkZakl { get; set; }
        [JsonProperty("sumZklZakl", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumZklZakl { get; set; }
        [JsonProperty("sumDphZakl", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumDphZakl { get; set; }

        [JsonProperty("sumCelkSniz", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumCelkSniz { get; set; }
        [JsonProperty("sumZklSniz", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumZklSniz { get; set; }
        [JsonProperty("sumDphSniz", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumDphSniz { get; set; }

        [JsonProperty("sumOsv", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumOsv { get; set; }

        [JsonProperty("sumCelkZaklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumCelkZaklMen { get; set; }
        [JsonProperty("sumZklZaklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumZklZaklMen { get; set; }
        [JsonProperty("sumDphZaklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumDphZaklMen { get; set; }

        [JsonProperty("sumCelkSnizMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumCelkSnizMen { get; set; }
        [JsonProperty("sumZklSnizMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumZklSnizMen { get; set; }
        [JsonProperty("sumDphSnizMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumDphSnizMen { get; set; }
        [JsonProperty("sumOsvMen", NullValueHandling = NullValueHandling.Ignore)]

        public decimal? SumOsvMen { get; set; }


        [JsonProperty("buc", NullValueHandling = NullValueHandling.Ignore)]
        public string CisloUctu { get; set; }
        [JsonProperty("smerKod", NullValueHandling = NullValueHandling.Ignore)]
        public string SmerKod { get; set; }
        [JsonProperty("typUcOp", NullValueHandling = NullValueHandling.Ignore)]
        public string? TypUcOp { get; set; }
        [JsonProperty("iban", NullValueHandling = NullValueHandling.Ignore)]
        public string Iban { get; set; }
        [JsonProperty("bic", NullValueHandling = NullValueHandling.Ignore)]
        public string Bic { get; set; }

        public void OpravaCen()
        {
            if (Mena == "code:CZK")
            {
                if (SumCelkem != null && Math.Abs(SumCelkem.Value - (SumCelkSniz ?? 0 + SumCelkZakl ?? 0 + SumOsv ?? 0)) < 1 && (SumOsv ?? 0) == 0)
                {
                    SumOsv = Math.Round(SumCelkem.Value - (SumCelkSniz ?? 0 + SumCelkZakl ?? 0), 2);
                }
            }
            else
            {
                if (SumCelkemMen != null && Math.Abs(SumCelkemMen.Value - (SumCelkSnizMen ?? 0 + SumCelkZaklMen ?? 0 + SumOsvMen ?? 0)) < 1 && (SumOsvMen ?? 0) == 0)
                {
                    SumOsv = Math.Round(SumCelkemMen.Value - (SumCelkSnizMen ?? 0 + SumCelkZaklMen ?? 0), 2);
                }
            }
        }
    }
}