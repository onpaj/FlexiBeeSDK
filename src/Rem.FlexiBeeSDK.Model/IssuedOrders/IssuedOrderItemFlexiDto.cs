using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model.IssuedOrders;

public class IssuedOrderItemFlexiDto
{
    [JsonProperty("id")] public string Id { get; set; }
    
    [JsonProperty("autogen")] public bool Autogenerate => false;

    [JsonProperty("cenJednotka")] public int PriceUnit => 1;

    [JsonProperty("cenik")] public string ProductCodeFormatted => $"code:{ProductCode}";

    [JsonProperty("cisRad")] public int DocumentLine => 1;

    [JsonProperty("kod")]
    public string ProductCode { get; set; }

    [JsonProperty("kopCinnost")] public bool CopyJob => true;

    [JsonProperty("kopDatTermin")] public bool CopyDate => true;

    [JsonProperty("kopKlice")] public bool CopyKeys => true;

    [JsonProperty("kopStred")] public bool CopyDepartment => true;

    [JsonProperty("kopZakazku")] public bool CopyOrder => true;

    [JsonProperty("mnozMj")]
    public double Ammount { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("sklad")]
    public string WarehouseCodeFormatted => $"code:{WarehouseCode}";
    
    public string WarehouseCode { get; set; }

    [JsonProperty("sarze")]
    public string? LotNumber { get; set; }

    [JsonProperty("expirace")]
    public DateTime? ExpirationDate { get; set; }

}