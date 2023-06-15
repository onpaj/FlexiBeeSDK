using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class IssuedInvoice
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
        public string Code { get; set; }

        [JsonProperty("stavUhrK", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentState { get; set; }

        [JsonProperty("stavUhrK@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentStateShowAs { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DateCreated { get; set; }

        [JsonProperty("datSplat", NullValueHandling = NullValueHandling.Ignore)]
        public string DateDue { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public string SumTotal { get; set; }

        [JsonProperty("sumZalohy", NullValueHandling = NullValueHandling.Ignore)]
        public string SumPrePayment { get; set; }

        [JsonProperty("sumZalohyMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumPrePaymentC { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumTotalC { get; set; }

        [JsonProperty("zbyvaUhraditMen", NullValueHandling = NullValueHandling.Ignore)]
        public string ToPayC { get; set; }

        [JsonProperty("zbyvaUhradit", NullValueHandling = NullValueHandling.Ignore)]
        public string ToPay { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyShowAs { get; set; }

        [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("polozkyFaktury@removeAll", NullValueHandling = NullValueHandling.Ignore)]
        public bool ItemsRemoveAll { get; set; } = true;

        [JsonProperty("polozkyDokladu", NullValueHandling = NullValueHandling.Ignore)]
        public List<IssuedInvoiceItem> Items { get; set; }

        [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
        public string VarSymbol { get; set; }

        [JsonProperty("cisObj", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }

        [JsonProperty("typDokl", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }
        [JsonProperty("duzpPuv", NullValueHandling = NullValueHandling.Ignore)]
        public string DateTaxOrig { get; set; }

        [JsonProperty("duzpUcto", NullValueHandling = NullValueHandling.Ignore)]
        public string DateTaxAcc { get; set; }
        
        [JsonProperty("nazFirmy", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }
        [JsonProperty("ulice", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyStreet { get; set; }
        [JsonProperty("mesto", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyCity { get; set; }
        [JsonProperty("stat", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyState { get; set; }
        [JsonProperty("ic", NullValueHandling = NullValueHandling.Ignore)]
        public string CIN { get; set; }
        [JsonProperty("dic", NullValueHandling = NullValueHandling.Ignore)]
        public string VATIN { get; set; }
        [JsonProperty("zaokrNaSumM", NullValueHandling = NullValueHandling.Ignore)]
        public string RoundingTotalC { get; set; }
        [JsonProperty("zaokrNaDphM", NullValueHandling = NullValueHandling.Ignore)]
        public string RoundingTaxC { get; set; }
        [JsonProperty("zaokrNaSumK", NullValueHandling = NullValueHandling.Ignore)]
        public string RoundingTotal { get; set; }
        [JsonProperty("zaokrNaDphK", NullValueHandling = NullValueHandling.Ignore)]
        public string RoundingTax { get; set; }
        [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
        public bool WithoutItems { get; set; }
        [JsonProperty("formaDopravy", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryType { get; set; }
        [JsonProperty("formaUhradyCis", NullValueHandling = NullValueHandling.Ignore)]
        public object PaymentType { get; set; }


        public void OpravaCen()
        {
            if (Currency == "code:CZK")
            {
            }
            else
            {
                RoundingTotal = "zaokrNa.setiny";
                RoundingTax = "zaokrNa.setiny";
                RoundingTotalC = "zaokrNa.setiny";
                RoundingTaxC = "zaokrNa.setiny";
            }
        }

        public void OpravaSkladu()
        {
            foreach(var p in Items)
                OpravaSkladu(p);
        }

        private void OpravaSkladu(IssuedInvoiceItem p)
        {
            if (p.Code == "DARBAL")
                p.Store = null;
        }

        public void MapovaniProduktu()
        {
            foreach (var p in Items)
            {
                if (string.IsNullOrEmpty(p.Code))
                    continue;
                
                // if (p.Kod.EndsWith("D"))
                // {
                //     p.Kod = p.Kod.Substring(-1);
                //     p.Cenik = $"code:{p.Kod}";
                // }

                if (ProductMap.ContainsKey(p.Code))
                {
                    p.Code = ProductMap[p.Code];
                    p.PriceList = $"code:{p.Code}";
                }
            }
        }
    }
}