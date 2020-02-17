﻿using Autofac;
using MAD.IntegrationFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.IntegrationFramework.Integrations
{
    internal class IntegrationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<FileSystemIntegrationMetaDataService>().As<IIntegrationMetaDataService>();

            builder.RegisterType<EntryAssemblyIntegrationResolver>().As<IIntegrationResolver>();

            builder.RegisterType<EntryAssemblyIntegrationScopeMIFDbContextResolver>().As<IIntegrationScopeMIFDbContextResolver>();
            builder.RegisterType<TimedIntegrationScopeFactory>().As<IIntegrationScopeFactory>();

            builder.RegisterType<EntryAssemblyMIFConfigResolver>().As<IMIFConfigResolver>();

            builder.RegisterType<TimedIntegrationRunAfterAttributeHandler>().AsSelf();

            builder.RegisterType<TimedIntegrationExecutionHandler>().AsSelf();
            builder.RegisterType<TimedIntegrationController>().InstancePerLifetimeScope().AsSelf();
        }
    }
}
