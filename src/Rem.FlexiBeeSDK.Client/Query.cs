﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Rem.FlexiBeeSDK.Client
{
    public class Query : RequestSpecs
    {
        public List<Relations> Relations { get; set; } = new List<Relations>();

        public string QueryString { get; set; }

        public IDictionary<string, string> Parameters { get; } = new Dictionary<string, string>();
        
        public override string ToString()
        {
            var q = $"{QueryString}.{FormatString}";

            var args = Parameters.Select(kvp => $"{kvp.Key}={kvp.Value}").ToList();

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