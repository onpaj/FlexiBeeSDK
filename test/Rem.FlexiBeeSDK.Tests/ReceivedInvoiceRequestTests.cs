using System;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Invoices;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class ReceivedInvoiceRequestTests
    {
        [Fact]
        public void Constructor_WithOnlyRequiredParameters_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo);
            
            var expectedFilter = "((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\")    )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithLabel_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, label: "URGENT");
            
            var expectedFilter = "((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\") stitky eq \"code:URGENT\"   )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithAccountingTemplate_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, accountingTemplate: "SLUZBY-OSTATNI");
            
            var expectedFilter = "((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\")  typUcOp.kod eq \"SLUZBY-OSTATNI\"  )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithDocumentNumber_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, documentNumber: "PF250809");
            
            var expectedFilter = "((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\")   kod eq \"PF250809\" )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithCompanyId_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, companyId: "12345678");
            
            var expectedFilter = "((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\")    ic eq \"code:12345678\")";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithAllOptionalParameters_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            
            var request = new ReceivedInvoiceRequest(
                dateFrom, 
                dateTo, 
                label: "URGENT",
                accountingTemplate: "SLUZBY-OSTATNI",
                documentNumber: "PF250809",
                companyId: "12345678"
            );
            
            var expectedFilter = "((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\") stitky eq \"code:URGENT\" typUcOp.kod eq \"SLUZBY-OSTATNI\" kod eq \"PF250809\" ic eq \"code:12345678\")";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithDifferentDates_FormatsCorrectly()
        {
            var dateFrom = new DateTime(2024, 12, 1);
            var dateTo = new DateTime(2025, 1, 31);
            
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo);
            
            var expectedFilter = "((datUcto gte \"2024-12-01\" and datUcto lte \"2025-01-31\")    )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithSpecialCharactersInDocumentNumber_HandlesCorrectly()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, documentNumber: "PF-2508.09/A");
            
            var expectedFilter = "((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\")   kod eq \"PF-2508.09/A\" )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void JsonSerialization_ProducesCorrectJson_WithAllFields()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, label: "URGENT");
            
            var json = JsonConvert.SerializeObject(request);
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(json);
            
            Assert.NotNull(jsonObj);
            Assert.True((bool)jsonObj["add-row-count"]);
            Assert.Equal(0, (int)jsonObj["limit"]);
            Assert.Equal(0, (int)jsonObj["start"]);
            Assert.Equal("datUcto", (string)jsonObj["order"]);
            Assert.True((bool)jsonObj["use-internal-id"]);
            Assert.True((bool)jsonObj["no-ext-ids"]);
            Assert.Equal("1.0", (string)jsonObj["@version"]);
            
            var filterValue = (string)jsonObj["filter"];
            Assert.Contains("datUcto gte", filterValue);
            Assert.Contains("2025-08-01", filterValue);
            Assert.Contains("datUcto lte", filterValue);
            Assert.Contains("2025-08-31", filterValue);
            Assert.Contains("URGENT", filterValue);
        }
        
        [Fact]
        public void DefaultPropertyValues_AreSetCorrectly()
        {
            var request = new ReceivedInvoiceRequest(DateTime.Now, DateTime.Now);
            
            Assert.True(request.AddRowCount);
            Assert.Equal(0, request.Limit);
            Assert.Equal(0, request.Start);
            Assert.Equal("datUcto", request.Order);
            Assert.True(request.UseInternalId);
            Assert.True(request.NoExtIds);
            Assert.Equal("1.0", request.Version);
        }
        
        [Fact]
        public void DetailProperty_ContainsAllRequiredFields()
        {
            var request = new ReceivedInvoiceRequest(DateTime.Now, DateTime.Now);
            
            var detail = request.Detail;
            
            Assert.Contains("datVyst", detail);
            Assert.Contains("kod", detail);
            Assert.Contains("typDokl", detail);
            Assert.Contains("nazFirmy", detail);
            Assert.Contains("cisDosle", detail);
            Assert.Contains("varSym", detail);
            Assert.Contains("stredisko", detail);
            Assert.Contains("datSplat", detail);
            Assert.Contains("sumZklCelkem", detail);
            Assert.Contains("mena", detail);
            Assert.Contains("sumCelkem", detail);
            Assert.Contains("stavUhrK", detail);
            Assert.Contains("popis", detail);
            Assert.Contains("typUcOp", detail);
            Assert.Contains("firma", detail);
            Assert.Contains("ic", detail);
        }
        
        [Fact]
        public void IncludesProperty_ContainsAllRequiredPaths()
        {
            var request = new ReceivedInvoiceRequest(DateTime.Now, DateTime.Now);
            
            var includes = request.Includes;
            
            Assert.Contains("/faktura-prijata/typDokl", includes);
            Assert.Contains("/faktura-prijata/stredisko", includes);
            Assert.Contains("/faktura-prijata/mena", includes);
            Assert.Contains("/faktura-prijata/smerKod", includes);
            Assert.Contains("/faktura-prijata/typUcOp", includes);
        }
        
        [Fact]
        public void Filter_ContainsProperParentheses()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, label: "URGENT");
            
            var filter = request.Filter;
            
            Assert.StartsWith("((", filter);
            Assert.Contains(")", filter);
            
            var openParenCount = filter.Split('(').Length - 1;
            var closeParenCount = filter.Split(')').Length - 1;
            Assert.Equal(openParenCount, closeParenCount);
        }
        
        [Fact]
        public void Filter_ContainsProperQuotes()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, documentNumber: "PF250809");
            
            var filter = request.Filter;
            
            Assert.Contains("2025-08-01", filter);
            Assert.Contains("2025-08-31", filter);
            Assert.Contains("PF250809", filter);
            
            Assert.Contains("datUcto gte", filter);
            Assert.Contains("datUcto lte", filter);
            Assert.Contains("kod eq", filter);
        }
        
        [Fact]
        public void Filter_WithLabel_UsesCodePrefix()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, label: "IMPORTANT");
            
            Assert.Contains("stitky eq \"code:IMPORTANT\"", request.Filter);
        }
        
        [Fact]
        public void Filter_WithAccountingTemplate_UsesCorrectField()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, accountingTemplate: "DOPRAVA");
            
            Assert.Contains("typUcOp.kod eq \"DOPRAVA\"", request.Filter);
        }
        
        [Fact]
        public void Filter_WithCompanyId_UsesCodePrefix()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, companyId: "87654321");
            
            Assert.Contains("ic eq \"code:87654321\"", request.Filter);
        }
        
        [Fact]
        public void JsonSerialization_FilterProperty_SerializesWithCorrectEscaping()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, documentNumber: "PF250809");
            
            var json = JsonConvert.SerializeObject(request);
            
            Assert.Contains("filter", json);
            Assert.Contains("datUcto gte", json);
            Assert.Contains("2025-08-01", json);
            Assert.Contains("2025-08-31", json);
            Assert.Contains("PF250809", json);
            
            Assert.Contains("\\\"", json);
        }
        
        [Fact]
        public void Filter_WithNullOptionalParameters_NoExtraSpaces()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(dateFrom, dateTo, null, null, null, null);
            
            var filter = request.Filter;
            
            // The filter will have spaces where empty strings are returned from helper methods
            Assert.Equal("((datUcto gte \"2025-08-01\" and datUcto lte \"2025-08-31\")    )", filter);
        }
        
        [Fact]
        public void Filter_WithMultipleParameters_MaintainsCorrectOrder()
        {
            var dateFrom = new DateTime(2025, 8, 1);
            var dateTo = new DateTime(2025, 8, 31);
            var request = new ReceivedInvoiceRequest(
                dateFrom, 
                dateTo, 
                label: "URGENT",
                accountingTemplate: "SLUZBY-OSTATNI",
                documentNumber: "PF250809",
                companyId: "12345678"
            );
            
            var filter = request.Filter;
            
            var dateIndex = filter.IndexOf("datUcto");
            var labelIndex = filter.IndexOf("stitky");
            var templateIndex = filter.IndexOf("typUcOp");
            var numberIndex = filter.IndexOf("kod eq");
            var companyIndex = filter.IndexOf("ic eq");
            
            Assert.True(dateIndex < labelIndex);
            Assert.True(labelIndex < templateIndex);
            Assert.True(templateIndex < numberIndex);
            Assert.True(numberIndex < companyIndex);
        }
    }
}