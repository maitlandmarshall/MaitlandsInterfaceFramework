﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.IntegrationFramework.Database
{
    internal class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(SqlServerMIFDbContextFactory<>)).As(typeof(IMIFDbContextFactory<>));
            builder.RegisterType<SqlServerMIFDbContextFactory>().As<IMIFDbContextFactory>();

            builder.RegisterType<AutomaticMigrationService>().As<IAutomaticMigrationService>().SingleInstance();
            builder.RegisterType(typeof(SqlStatementBuilder)).AsSelf();
            
            builder.RegisterType(typeof(MIFDbContextBuilder)).AsSelf();
            builder.RegisterGeneric(typeof(MIFDbContextBuilder<>)).AsSelf();
        }
    }
}
