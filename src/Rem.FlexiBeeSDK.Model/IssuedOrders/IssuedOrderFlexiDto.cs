using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class IssuedOrderFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("datVyst")]
    public string Date { get; set; }

    [JsonProperty("kod")]
    public string Code { get; set; }

    [JsonProperty("typDokl")]
    public List<IssuedOrderDocumentTypeFlexiDto> DocumentTypeList { get; set; }

    public IssuedOrderDocumentTypeFlexiDto DocumentType => DocumentTypeList.First();

    [JsonProperty("nazFirmy")]
    public string CompanyName { get; set; }

    [JsonProperty("stredisko")]
    public List<IssuedOrderDepartmentFlexiDto> DepartmentList { get; set; }
        
    public IssuedOrderDepartmentFlexiDto? Department => DepartmentList.FirstOrDefault();

    [JsonProperty("sumZklCelkemMen")]
    public double PriceWithoutVatCurrency { get; set; }

    [JsonProperty("sumZklCelkem")]
    public double PriceWithoutVat { get; set; }

    [JsonProperty("mena@showAs")]
    public string CurrencyCode { get; set; }

    [JsonProperty("sumCelkemMen")]
    public double PriceCurrency { get; set; }

    [JsonProperty("sumCelkem")]
    public double Price { get; set; }

    [JsonProperty("storno")]
    public bool Cancelled { get; set; }


    [JsonProperty("stavDoklObch")]
    public List<IssuedOrderStateFlexiDto> StateList { get; set; }
        
    IssuedOrderStateFlexiDto State => StateList.First();

    [JsonProperty("popis")]
    public string Description { get; set; }
    
    [JsonProperty("polozkyObchDokladu")]
    public List<IssuedOrderItemFlexiDto> Items { get; set; }
}