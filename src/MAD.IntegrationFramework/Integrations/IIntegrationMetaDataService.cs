﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.IntegrationFramework.Integrations
{
    internal interface IIntegrationMetaDataService
    {
        void Save(TimedIntegration timedInterface);
        void Load(TimedIntegration timedIntegration);
    }
}