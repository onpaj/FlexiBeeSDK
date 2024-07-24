using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client
{
    public class WinstromEnvelope<T>
    {
        [JsonProperty("winstrom")]
        public T Data { get; set; }
    }
}
