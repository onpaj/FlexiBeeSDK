using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;

namespace Rem.FlexiBeeSDK.Client
{
    public class WinstromEnvelope<T>
    {
        public Winstrom<T> Winstrom { get; set; }
    }

    public class Winstrom<T>
    {
        public string Version { get; set; }
        
        public IDictionary<string, List<T>> Value { get; set; }
    }
}
