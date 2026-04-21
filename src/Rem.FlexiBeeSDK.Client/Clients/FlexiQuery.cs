using System.Collections.Generic;

namespace Rem.FlexiBeeSDK.Client.Clients;

public class FlexiQuery
{
    public IDictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// When false, query parameters are appended directly to the resource URL (?key=value)
    /// instead of using the FlexiBee /query endpoint path segment.
    /// </summary>
    public bool IncludeQuerySegment { get; set; } = true;
}