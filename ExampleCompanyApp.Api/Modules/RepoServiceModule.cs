

using Autofac;
using ExampleCompanyApp.Api.Filters;
using ExampleCompanyApp.Caching;
using ExampleCompanyApp.Core.Models;
using ExampleCompanyApp.Core.Repositories;
using ExampleCompanyApp.Core.Services;
using ExampleCompanyApp.Core.UnitOfWorks;
using ExampleCompanyApp.Repository;
using ExampleCompanyApp.Repository.Repositories;
using ExampleCompanyApp.Repository.UnitOfWorks;
using ExampleCompanyApp.Service.Mapping;
using ExampleCompanyApp.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace ExampleCompanyApp.Api.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repositoryAssembly = Assembly.GetAssembly(typeof(ExampleCompanyDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
                .Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
                .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

            builder.RegisterGeneric(typeof(NotFoundFilter<>))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithCaching>()
                .As<IProductService>()
                .As<IService<Product>>()
                .InstancePerLifetimeScope();

        }

        
    }
}
