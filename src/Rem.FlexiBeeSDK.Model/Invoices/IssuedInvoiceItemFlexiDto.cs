using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices
{
    public class IssuedInvoiceItemFlexiDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
        [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DateCreated { get; set; }

        [JsonProperty("cenik", NullValueHandling = NullValueHandling.Ignore)]
        public string PriceList { get; set; }

        [JsonProperty("sklad", NullValueHandling = NullValueHandling.Ignore)]
        public string Store { get; set; }

        [JsonProperty("mnozMj", NullValueHandling = NullValueHandling.Ignore)]
        public string Amount { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumTotal { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumTotalC { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyShowAs { get; set; }
        
        [JsonProperty("mj", NullValueHandling = NullValueHandling.Ignore)]
        public string MeasureUnit { get; set; }
        [JsonProperty("cenaMj", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PricePerUnit { get; set; }
        [JsonProperty("sumZklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumBaseC { get; set; }
        [JsonProperty("sumZkl", NullValueHandling = NullValueHandling.Ignore)] 
        public decimal? SumBase { get; set; }
        [JsonProperty("typSzbDphK", NullValueHandling = NullValueHandling.Ignore)]
        public string VatRateType { get; set; }
        [JsonProperty("typCenyDphK", NullValueHandling = NullValueHandling.Ignore)] 
        public string PriceVatType { get; set; }
        
        [JsonProperty("zklMdUcet", NullValueHandling = NullValueHandling.Ignore)] 
        public string AccountBaseMd { get; set; }
        
        [JsonProperty("zklDalUcet", NullValueHandling = NullValueHandling.Ignore)] 
        public string AccountBaseDal { get; set; }
        
        [JsonProperty("dphMdUcet", NullValueHandling = NullValueHandling.Ignore)] 
        public string AccountVatMd { get; set; }
        
        [JsonProperty("dphDalUcet", NullValueHandling = NullValueHandling.Ignore)] 
        public string AccountVatDal { get; set; }
        
        [JsonProperty("clenKonVykDph", NullValueHandling = NullValueHandling.Ignore)] 
        public string CategoryVatReport { get; set; }

        [JsonProperty("kopClenKonVykDph", NullValueHandling = NullValueHandling.Ignore)]
        public bool CopyCategoryVatReport { get; set; } = true;
        
        [JsonProperty("clenDph", NullValueHandling = NullValueHandling.Ignore)] 
        public string CategoryVat { get; set; }

        [JsonProperty("kopClenDph", NullValueHandling = NullValueHandling.Ignore)]
        public bool CopyCategoryVat { get; set; } = true;
    }
}