using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class FakturaVydana
    {
        private static Dictionary<string, string> ProductMap = new Dictionary<string, string>
        {
            { "1074/100", "SEZ001100" },
        };
        
        
        
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

        [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
        public string Popis { get; set; }

        [JsonProperty("polozkyFaktury@removeAll", NullValueHandling = NullValueHandling.Ignore)]
        public bool PolozkyDokladuReplace { get; set; } = true;

        [JsonProperty("polozkyDokladu", NullValueHandling = NullValueHandling.Ignore)]
        public List<PolozkaFakturyVydane> PolozkyDokladu { get; set; }

        [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
        public string VarSym { get; set; }

        [JsonProperty("cisObj", NullValueHandling = NullValueHandling.Ignore)]
        public string CisObj { get; set; }

        [JsonProperty("typDokl", NullValueHandling = NullValueHandling.Ignore)]
        public string TypDokl { get; set; }
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
        [JsonProperty("zaokrNaSumM", NullValueHandling = NullValueHandling.Ignore)]
        public string ZaokrNaSumM { get; set; }
        [JsonProperty("zaokrNaDphM", NullValueHandling = NullValueHandling.Ignore)]
        public string ZaokrNaDphM { get; set; }
        [JsonProperty("zaokrNaSumK", NullValueHandling = NullValueHandling.Ignore)]
        public string ZaokrNaSumK { get; set; }
        [JsonProperty("zaokrNaDphK", NullValueHandling = NullValueHandling.Ignore)]
        public string ZaokrNaDphK { get; set; }
        [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
        public bool BezPolozek { get; set; }
        [JsonProperty("formaDopravy", NullValueHandling = NullValueHandling.Ignore)]
        public string FormaDopravy { get; set; }
        [JsonProperty("formaUhradyCis", NullValueHandling = NullValueHandling.Ignore)]
        public object FormaUhradyCis { get; set; }


        public void OpravaCen()
        {
            if (Mena == "code:CZK")
            {
            }
            else
            {
                ZaokrNaSumK = "zaokrNa.setiny";
                ZaokrNaDphK = "zaokrNa.setiny";
                ZaokrNaSumM = "zaokrNa.setiny";
                ZaokrNaDphM = "zaokrNa.setiny";
            }
        }

        public void OpravaSkladu()
        {
            foreach(var p in PolozkyDokladu)
                OpravaSkladu(p);
        }

        private void OpravaSkladu(PolozkaFakturyVydane p)
        {
            if (p.Kod == "DARBAL")
                p.Sklad = null;
        }

        public void MapovaniProduktu()
        {
            foreach (var p in PolozkyDokladu)
            {
                if (string.IsNullOrEmpty(p.Kod))
                    continue;
                
                // if (p.Kod.EndsWith("D"))
                // {
                //     p.Kod = p.Kod.Substring(-1);
                //     p.Cenik = $"code:{p.Kod}";
                // }

                if (ProductMap.ContainsKey(p.Kod))
                {
                    p.Kod = ProductMap[p.Kod];
                    p.Cenik = $"code:{p.Kod}";
                }
            }
        }
    }
}