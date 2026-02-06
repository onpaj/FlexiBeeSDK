using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model;

public class BoMItemFlexiDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("lastUpdate")]
    public DateTime LastUpdate { get; set; }

    [JsonProperty("nazev")]
    public string Name { get; set; }

    [JsonProperty("mnoz")]
    public double Amount { get; set; }

    [JsonProperty("hladina")]
    public int Level { get; set; }

    [JsonProperty("poradi")]
    public int Order { get; set; }

    [JsonProperty("cesta")]
    public string Path { get; set; }

    public string? ParentCode => ParentProduct?.Code;
        
    [JsonProperty("otecCenik")]
    public List<BomProductFlexiDto> ParentProductList { get; set; } =  new List<BomProductFlexiDto>();
    public BomProductFlexiDto? ParentProduct => ParentProductList.FirstOrDefault();
    public string? ParentFullName => ParentProduct?.Name;
    public int? ParentTemplateId => Parent?.Id;
        
        
        
    [JsonProperty("cenik")]
    public List<BomProductFlexiDto> Ingredient { get; set; } = new List<BomProductFlexiDto>();
    public string IngredientCode => Ingredient.FirstOrDefault()!.Code;
    public string IngredientFullName => Ingredient.FirstOrDefault()!.Name;
    public bool HasLots => Ingredient.FirstOrDefault()!.HasLots;
    public bool HasExpiration => Ingredient.FirstOrDefault()!.HasExpiration;
        
    [JsonProperty("otec")]
    public List<ParentBomFlexiDto> ParentList { get; set; }

    public ParentBomFlexiDto? Parent => ParentList?.FirstOrDefault();
    
    [JsonProperty("otec@showAs")]
    public string ParentTemplateName { get; set; }
}