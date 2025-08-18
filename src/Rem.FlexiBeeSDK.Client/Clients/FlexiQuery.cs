using System.Collections.Generic;

namespace Rem.FlexiBeeSDK.Client.Clients;

public class FlexiQuery
{
    public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
}