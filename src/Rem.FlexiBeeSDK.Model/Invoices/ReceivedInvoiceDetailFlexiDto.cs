using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices
{
    public class ReceivedInvoiceDetailFlexiDto : IValidate
    {
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
        public decimal? SumTotal { get; set; }

        [JsonProperty("sumZalohy", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumPrePayment { get; set; }

        [JsonProperty("sumZalohyMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumPrePaymentC { get; set; }

        [JsonProperty("sumCelkemMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumTotalC { get; set; }

        [JsonProperty("zbyvaUhraditMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ToPayC { get; set; }

        [JsonProperty("zbyvaUhradit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ToPay { get; set; }

        [JsonProperty("mena", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("mena@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyRef { get; set; }

        [JsonProperty("mena@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyShowAs { get; set; }

        [JsonProperty("firma", NullValueHandling = NullValueHandling.Ignore)]
        public string Company { get; set; }

        [JsonProperty("firma@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyRef { get; set; }

        [JsonProperty("firma@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyShowAs { get; set; }

        [JsonProperty("popis", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("polozkyDokladu", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReceivedInvoiceItemFlexiDto> Items { get; set; }

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
        [JsonProperty("varSym", NullValueHandling = NullValueHandling.Ignore)]
        public string VarSymbol { get; set; }
        [JsonProperty("bezPolozek", NullValueHandling = NullValueHandling.Ignore)]
        public bool WithoutItems { get; set; }
        [JsonProperty("typDokl", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }
        
        [JsonProperty("cisDosle", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentNumberReceived { get; set; }
        [JsonProperty("psc", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyZipCode { get; set; }


        [JsonProperty("sumCelkZakl", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumTotalDefaultVat { get; set; }
        [JsonProperty("sumZklZakl", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumWithoutVatDefaultVat { get; set; }
        [JsonProperty("sumDphZakl", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumVatDefaultVat { get; set; }

        [JsonProperty("sumCelkSniz", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumTotalLimitedVat { get; set; }
        [JsonProperty("sumZklSniz", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumWithoutVatLimitedVat { get; set; }
        [JsonProperty("sumDphSniz", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumVatLimitedVat { get; set; }

        [JsonProperty("sumOsv", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumNoVat { get; set; }

        [JsonProperty("sumCelkZaklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumTotalDefaultVatC { get; set; }
        [JsonProperty("sumZklZaklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumWithoutVatDefaultVatC { get; set; }
        [JsonProperty("sumDphZaklMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumVatDefaultVatC { get; set; }

        [JsonProperty("sumCelkSnizMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumTotalLimitedVatC { get; set; }
        [JsonProperty("sumZklSnizMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumWithoutVatLimitedVatC { get; set; }
        [JsonProperty("sumDphSnizMen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SumVatLimitedVatC { get; set; }
        [JsonProperty("sumOsvMen", NullValueHandling = NullValueHandling.Ignore)]

        public decimal? SumNoVatC { get; set; }


        [JsonProperty("buc", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccountNumber { get; set; }
        [JsonProperty("smerKod", NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }
        [JsonProperty("typUcOp", NullValueHandling = NullValueHandling.Ignore)]
        public string? TransactionType { get; set; }
        [JsonProperty("iban", NullValueHandling = NullValueHandling.Ignore)]
        public string Iban { get; set; }
        [JsonProperty("bic", NullValueHandling = NullValueHandling.Ignore)]
        public string Bic { get; set; }

        public void OpravaCen()
        {
            if (Currency == "code:CZK")
            {
                if (SumTotal != null && Math.Abs(SumTotal.Value - (SumTotalLimitedVat ?? 0 + SumTotalDefaultVat ?? 0 + SumNoVat ?? 0)) < 1 && (SumNoVat ?? 0) == 0)
                {
                    SumNoVat = Math.Round(SumTotal.Value - (SumTotalLimitedVat ?? 0 + SumTotalDefaultVat ?? 0), 2);
                }
            }
            else
            {
                if (SumTotalC != null && Math.Abs(SumTotalC.Value - (SumTotalLimitedVatC ?? 0 + SumTotalDefaultVatC ?? 0 + SumNoVatC ?? 0)) < 1 && (SumNoVatC ?? 0) == 0)
                {
                    SumNoVat = Math.Round(SumTotalC.Value - (SumTotalLimitedVatC ?? 0 + SumTotalDefaultVatC ?? 0), 2);
                }
            }
        }

        public void Validate()
        {
            
        }
    }
}