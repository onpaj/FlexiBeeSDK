using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.Invoices;
    public class ReceivedInvoiceFlexiDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("datVyst")]
        public DateTime? IssueDate { get; set; }

        [JsonProperty("kod")]
        public string Code { get; set; }

        [JsonProperty("typDokl@evidencePath")]
        public string DocumentTypeEvidencePath { get; set; }

        [JsonProperty("typDokl@internalId")]
        public int DocumentTypeInternalId { get; set; }

        [JsonProperty("typDokl@ref")]
        public string DocumentTypeRef { get; set; }

        [JsonProperty("typDokl@showAs")]
        public string DocumentTypeShowAs { get; set; }


        [JsonProperty("nazFirmy")]
        public string CompanyName { get; set; }

        [JsonProperty("cisDosle")]
        public string ReceivedNumber { get; set; }

        [JsonProperty("varSym")]
        public string VariableSymbol { get; set; }

        [JsonProperty("stredisko@evidencePath")]
        public string DepartmentEvidencePath { get; set; }

        [JsonProperty("stredisko@internalId")]
        public int DepartmentInternalId { get; set; }

        [JsonProperty("stredisko@ref")]
        public string DepartmentRef { get; set; }

        [JsonProperty("stredisko@showAs")]
        public string DepartmentShowAs { get; set; }

        [JsonProperty("stredisko")]
        public List<DepartmentFlexiDto> DepartmentList { get; set; }
        
        public DepartmentFlexiDto? Department =>  DepartmentList?.FirstOrDefault();

        [JsonProperty("datSplat")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("sumZklCelkemMen")]
        public double TotalBaseAmountForeign { get; set; }

        [JsonProperty("sumZklCelkem")]
        public double TotalBaseAmount { get; set; }

        [JsonProperty("mena@evidencePath")]
        public string CurrencyEvidencePath { get; set; }

        [JsonProperty("mena@internalId")]
        public int CurrencyInternalId { get; set; }

        [JsonProperty("mena@ref")]
        public string CurrencyRef { get; set; }

        [JsonProperty("mena@showAs")]
        public string CurrencyShowAs { get; set; }

        [JsonProperty("mena")]
        public List<CurrencyFlexiDto> CurrencyList { get; set; }

        public CurrencyFlexiDto Currency => CurrencyList.Single();

        
        [JsonProperty("sumCelkemMen")]
        public double TotalAmountForeign { get; set; }

        [JsonProperty("sumCelkem")]
        public double TotalAmount { get; set; }

        [JsonProperty("juhSum")]
        public double RemainingAmount { get; set; }

        [JsonProperty("stavUhrK")]
        public string PaymentStatus { get; set; }

        [JsonProperty("stavUhrK@showAs")]
        public string PaymentStatusShowAs { get; set; }

        [JsonProperty("juhSumMen")]
        public double RemainingAmountForeign { get; set; }

        [JsonProperty("storno")]
        public bool IsCancelled { get; set; }

        [JsonProperty("popis")]
        public string Description { get; set; }

        [JsonProperty("buc")]
        public string BankAccountNumber { get; set; }

        [JsonProperty("smerKod@evidencePath")]
        public string BankCodeEvidencePath { get; set; }

        [JsonProperty("smerKod@internalId")]
        public int BankCodeInternalId { get; set; }

        [JsonProperty("smerKod@ref")]
        public string BankCodeRef { get; set; }

        [JsonProperty("smerKod@showAs")]
        public string BankCodeShowAs { get; set; }

        [JsonProperty("iban")]
        public string IBAN { get; set; }

        [JsonProperty("bic")]
        public string BIC { get; set; }

        [JsonProperty("zuctovano")]
        public bool IsAccounted { get; set; }

        [JsonProperty("datUcto")]
        public DateTime? AccountingDate { get; set; }

        [JsonProperty("typUcOp@evidencePath")]
        public string AccountingTemplateEvidencePath { get; set; }

        [JsonProperty("typUcOp@internalId")]
        public int AccountingTemplateInternalId { get; set; }

        [JsonProperty("typUcOp@ref")]
        public string AccountingTemplateRef { get; set; }

        [JsonProperty("typUcOp@showAs")]
        public string AccountingTemplateShowAs { get; set; }

        [JsonProperty("typUcOp")]
        public List<AccountTemplateFlexiDto> AccountingTemplateList { get; set; }
        
        public AccountTemplateFlexiDto? AccountingTemplate => AccountingTemplateList.FirstOrDefault();

        [JsonProperty("podpisPrik")]
        public bool IsSignedCommand { get; set; }

        [JsonProperty("zamekK")]
        public string LockStatus { get; set; }

        [JsonProperty("zamekK@showAs")]
        public string LockStatusShowAs { get; set; }

        [JsonProperty("stavOdpocetK")]
        public string DeductionStatus { get; set; }

        [JsonProperty("stavUzivK")]
        public string UserStatus { get; set; }

        [JsonProperty("stavUzivK@showAs")]
        public string UserStatusShowAs { get; set; }

        [JsonProperty("bezPolozek")]
        public bool WithoutItems { get; set; }

        [JsonProperty("firma")]
        public string Company { get; set; }

        [JsonProperty("firma@evidencePath")]
        public string CompanyEvidencePath { get; set; }

        [JsonProperty("firma@internalId")]
        public int CompanyInternalId { get; set; }

        [JsonProperty("firma@ref")]
        public string CompanyRef { get; set; }

        [JsonProperty("firma@showAs")]
        public string CompanyShowAs { get; set; }

        [JsonProperty("ic")]
        public string CompanyIdNumber { get; set; }
        
        [JsonProperty("stitky")]
        public string Labels { get; set; }

    }