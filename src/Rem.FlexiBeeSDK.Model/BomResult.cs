using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Model
{
    public class BomResult
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("kusovnik")]
        public List<BoMItemFlexiDto> BoMItems { get; set; }
    }
}
