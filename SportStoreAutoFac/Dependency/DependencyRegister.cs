using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SportStoreAutoFac.Data;
using SportStoreAutoFac.Services;

namespace SportStoreAutoFac.Dependency
{
    public class DependencyRegister:Module
    {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IAsyncRepository<>)).InstancePerLifetimeScope();


            //builder.RegisterType<EFOrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<EFStoreRepository>().As<IStoreRepository>().InstancePerLifetimeScope();
        }
    }
}
