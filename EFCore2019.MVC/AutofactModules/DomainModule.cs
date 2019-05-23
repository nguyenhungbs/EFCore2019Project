using Autofac;
using EFCore2019.Domain.Repositories.Test;
using EFCore2019.Domain.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2019.MVC.AutofactModules
{
    public class DomainModule : Module
    {
        public DomainModule(/*string stringConn*/)
        {
            //_stringConn = stringConn;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //builder.Register(c=>new SqlConnection(_stringConn)).As<IDbConnection>().InstancePerLifetimeScope();

            builder.RegisterType<TestRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<TestService>().AsImplementedInterfaces();
        }
    }
}
