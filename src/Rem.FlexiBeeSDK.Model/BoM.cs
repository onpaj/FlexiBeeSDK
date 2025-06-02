using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class BoMProduct
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("kod")]
        public string Code { get; set; }

        [JsonProperty("nazev")]
        public string Name { get; set; }

        [JsonProperty("nakupCena")]
        public double PurchasePrice { get; set; }

        [JsonProperty("popis")]
        public string Description { get; set; }
    }

    public class BoMItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mnoz")]
        public double Amount { get; set; }

        [JsonProperty("hladina")]
        public int Level { get; set; }

        [JsonProperty("poradi")]
        public int Order { get; set; }

        [JsonProperty("cesta")]
        public string Path { get; set; }

        [JsonProperty("cenik")]
        public List<BoMProduct> Products { get; set; }
        
        [JsonProperty("otecCenik")]
        public List<BoMProduct> MasterProducts { get; set; }

        [JsonProperty("nazev")]
        public string Name { get; set; }
    }
    
    public class BomList
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("kusovnik")]
        public List<BoMItem> BoM { get; set; }
    }
    
    
    public class BoMItemV2
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

        [JsonProperty("otecCenik")]
        public string ParentCode { get; set; }

        [JsonProperty("otecCenik@showAs")]
        public string ParentFullName { get; set; }

        [JsonProperty("cenik")]
        public string IngredientCode { get; set; }

        [JsonProperty("cenik@showAs")]
        public string IngredientFullName { get; set; }

        [JsonProperty("otec")]
        public int? ParentTemplateId { get; set; }

        [JsonProperty("otec@showAs")]
        public string ParentTemplateName { get; set; }
    }
}
