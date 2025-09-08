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

    //[JsonProperty("otecCenik")]
    public string? ParentCode => ParentProduct.FirstOrDefault()?.Code;
        
    [JsonProperty("otecCenik")]
    public List<BomProductFlexiDto> ParentProduct { get; set; } =  new List<BomProductFlexiDto>();
    public string? ParentFullName => ParentProduct.FirstOrDefault()?.Name;
    public int? ParentTemplateId => Parent.FirstOrDefault()?.Id;
        
        
        
    [JsonProperty("cenik")]
    public List<BomProductFlexiDto> Ingredient { get; set; } = new List<BomProductFlexiDto>();
        
    public string IngredientCode => Ingredient.FirstOrDefault()!.Code;
    public string IngredientFullName => Ingredient.FirstOrDefault()!.Name;

        
    [JsonProperty("otec")]
    public List<ParentBomFlexiDto> Parent { get; set; }

    [JsonProperty("otec@showAs")]
    public string ParentTemplateName { get; set; }
}