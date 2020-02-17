﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.IntegrationFramework.Configuration
{
    internal class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<FileSystemMIFConfigFactory>().As<IMIFConfigFactory>();
            builder.RegisterType<FileSystemMIFConfigRepository>().As<IMIFConfigRepository>();

            builder.Register<MIFConfig>(context => context.Resolve<IMIFConfigFactory>().Create()).InstancePerDependency().AsSelf();
        }
    }
}
