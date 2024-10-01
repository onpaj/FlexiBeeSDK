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
}
