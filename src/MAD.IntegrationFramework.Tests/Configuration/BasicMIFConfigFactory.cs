﻿using MAD.IntegrationFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.IntegrationFramework.Tests.Configuration
{
    internal class BasicMIFConfigFactory : IMIFConfigFactory
    {
        public MIFConfig Create()
        {
            return new MIFConfig();
        }
    }
}
