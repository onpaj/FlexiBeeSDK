using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Invoices;

namespace Rem.FlexiBeeSDK.Model
{
    [Obsolete]
    public class ObjednavkaVydana
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Kod { get; set; }

        [JsonProperty("datVyst", NullValueHandling = NullValueHandling.Ignore)]
        public string DatVyst { get; set; }

        [JsonProperty("sumCelkem", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkem { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public string SumCelkemMen { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Mena { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string MenaShowAs { get; set; }

        [JsonProperty("firma", NullValueHandling = NullValueHandling.Ignore)]
        public string Firma { get; set; }

        [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
        public string Popis { get; set; }

        [JsonProperty("polozkyDokladu", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReceivedInvoiceItem> PolozkyDokladu { get; set; }

        [JsonProperty("vazebni-doklady", NullValueHandling = NullValueHandling.Ignore)]
        public List<VazebniDoklad> VazebniDoklady { get; set; }
    }
}