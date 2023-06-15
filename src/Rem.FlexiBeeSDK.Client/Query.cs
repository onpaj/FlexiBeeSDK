using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rem.FlexiBeeSDK.Client
{
    public class Query : RequestSpecs
    {
        public List<Relations> Relations { get; set; } = new List<Relations>();

        public string QueryString { get; set; }

        public override string ToString()
        {
            var q = $"({QueryString}).{FormatString}";
            
            var args = new List<string>();

            if(LevelOfDetail != LevelOfDetail.Undefined)
                args.Add($"detail={LevelOfDetailString}");

            if (Relations.Any())
            {
                args.Add($"relations={string.Join(",", Relations)}");
            }

            for (int i = 0; i < args.Count; i++)
            {
                q += i == 0 ? "?" : "&";
                q += args[i];
            }

            return q;
        }
    }
}