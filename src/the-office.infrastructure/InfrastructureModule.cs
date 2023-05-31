using Autofac;
using the_office.infrastructure.Data.Context;
using System.Reflection;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Repositories;

namespace the_office.infrastructure;

public class InfrastructureModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(TheOfficeDbContext).GetTypeInfo().Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<TheOfficeDbContext>()
            .AsSelf()
            .InstancePerLifetimeScope();       
    }
}