﻿using System.Collections.Generic;

namespace Rem.FlexiBeeSDK.Client
{
    public class FlexiBeeSettings
    {
        public const string ConfigNodeName = "FlexiBeeSettings";
        
        public string Server { get; set; }
        public string Company { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}