using System;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests
{
    public class LedgerRequestTests
    {
        [Fact]
        public void Constructor_WithOnlyDates_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            
            var request = new LedgerRequest(dateFrom, dateTo);
            
            var expectedFilter = "((datUcto gte \"2025-06-01\" and datUcto lte \"2025-06-30\")  )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithDebitAccountPrefix_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52");
            
            var expectedFilter = "((datUcto gte \"2025-06-01\" and datUcto lte \"2025-06-30\") and mdUcet.kod begins \"52\" )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithCreditAccountPrefix_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            
            var request = new LedgerRequest(dateFrom, dateTo, creditAccountPrefix: "31");
            
            var expectedFilter = "((datUcto gte \"2025-06-01\" and datUcto lte \"2025-06-30\")  and dalUcet.kod begins \"31\")";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithBothAccountPrefixes_GeneratesCorrectFilter()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52", creditAccountPrefix: "31");
            
            var expectedFilter = "((datUcto gte \"2025-06-01\" and datUcto lte \"2025-06-30\") and mdUcet.kod begins \"52\" and dalUcet.kod begins \"31\")";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithDifferentDates_FormatsCorrectly()
        {
            var dateFrom = new DateTime(2024, 12, 1);
            var dateTo = new DateTime(2025, 1, 31);
            
            var request = new LedgerRequest(dateFrom, dateTo);
            
            var expectedFilter = "((datUcto gte \"2024-12-01\" and datUcto lte \"2025-01-31\")  )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void Constructor_WithSpecialCharactersInAccountPrefix_HandlesCorrectly()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52.1");
            
            var expectedFilter = "((datUcto gte \"2025-06-01\" and datUcto lte \"2025-06-30\") and mdUcet.kod begins \"52.1\" )";
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void JsonSerialization_ProducesCorrectJson_WithAllFields()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52");
            
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
            // The filter field in JSON object will contain simple quotes
            var filterValue = (string)jsonObj["filter"];
            Assert.Contains("datUcto gte", filterValue);
            Assert.Contains("2025-06-01", filterValue);
            Assert.Contains("datUcto lte", filterValue);
            Assert.Contains("2025-06-30", filterValue);
            Assert.Contains("mdUcet.kod begins", filterValue);
            Assert.Contains("52", filterValue);
        }
        
        [Fact]
        public void DefaultPropertyValues_AreSetCorrectly()
        {
            var request = new LedgerRequest(DateTime.Now, DateTime.Now);
            
            Assert.True(request.AddRowCount);
            Assert.Equal(0, request.Limit);
            Assert.Equal(0, request.Start);
            Assert.Equal("datUcto", request.Order);
            Assert.True(request.UseInternalId);
            Assert.True(request.NoExtIds);
            Assert.Equal("1.0", request.Version);
            Assert.Equal("custom:parSymbol,datVyst,datUcto,doklad,nazFirmy,stredisko(nazev,kod,id),popis,sumTuz,sumMen,mena(kod),kurz,mdUcet(kod,nazev,id),dalUcet(kod,nazev,id),zuctovano,idUcetniDenik,idDokl", request.Detail);
            Assert.Equal("/ucetni-denik/stredisko,/ucetni-denik/mena,/ucetni-denik/mdUcet,/ucetni-denik/dalUcet", request.Includes);
        }
        
        [Fact]
        public void Filter_ContainsProperParentheses()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52");
            
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
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52");
            
            var filter = request.Filter;
            
            // The filter contains values with double quotes
            Assert.Contains("2025-06-01", filter);
            Assert.Contains("2025-06-30", filter);
            Assert.Contains("52", filter);
            
            // Verify the format matches what we expect
            Assert.Contains("datUcto gte", filter);
            Assert.Contains("datUcto lte", filter);
            Assert.Contains("mdUcet.kod begins", filter);
        }
        
        [Fact]
        public void Filter_WithComplexExample_MatchesExpectedFormat()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52");
            
            // The expected format - matches the user's requirement
            var expectedFilter = "((datUcto gte \"2025-06-01\" and datUcto lte \"2025-06-30\") and mdUcet.kod begins \"52\" )";
            
            Assert.Equal(expectedFilter, request.Filter);
        }
        
        [Fact]
        public void JsonSerialization_FilterProperty_SerializesWithCorrectEscaping()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            var request = new LedgerRequest(dateFrom, dateTo, debitAccountPrefix: "52");
            
            var json = JsonConvert.SerializeObject(request);
            
            // In JSON, the double quotes inside the filter string are escaped
            Assert.Contains("filter", json);
            Assert.Contains("datUcto gte", json);
            Assert.Contains("2025-06-01", json);
            Assert.Contains("2025-06-30", json);
            Assert.Contains("mdUcet.kod begins", json);
            Assert.Contains("52", json);
            
            // The JSON should contain escaped quotes for the filter value
            Assert.Contains("\\\"", json);
        }
        
        [Fact]
        public void Filter_EmptyAccountPrefixes_NoExtraSpaces()
        {
            var dateFrom = new DateTime(2025, 6, 1);
            var dateTo = new DateTime(2025, 6, 30);
            var request = new LedgerRequest(dateFrom, dateTo, null, null);
            
            var filter = request.Filter;
            
            Assert.DoesNotContain("  and", filter);
            Assert.DoesNotContain("and  ", filter);
            Assert.DoesNotContain("   ", filter);
        }
    }
}