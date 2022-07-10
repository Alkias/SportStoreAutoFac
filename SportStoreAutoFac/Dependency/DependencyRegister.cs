using Autofac;
using SportStoreAutoFac.Data;
using SportStoreAutoFac.Services;

namespace SportStoreAutoFac.Dependency
{
    public class DependencyRegister : Module
    {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IAsyncRepository<>)).InstancePerLifetimeScope();
        }
    }
}