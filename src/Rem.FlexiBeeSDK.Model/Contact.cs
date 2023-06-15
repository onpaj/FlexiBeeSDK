using System;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class Contact
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lastUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdate { get; set; }

        [JsonProperty("kod", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("nazev", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("ic", NullValueHandling = NullValueHandling.Ignore)]
        public string CIN { get; set; }

        [JsonProperty("dic", NullValueHandling = NullValueHandling.Ignore)]
        public string VATIN { get; set; }

        [JsonProperty("ulice", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty("mesto", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("psc", NullValueHandling = NullValueHandling.Ignore)]
        public long? ZipCode { get; set; }

        [JsonProperty("stat", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("stat@ref", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryRef { get; set; }

        [JsonProperty("stat@showAs", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryShowAs { get; set; }
    }
}