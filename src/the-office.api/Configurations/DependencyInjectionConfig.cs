using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using the_office.api.application.Common;
using the_office.api.application.Common.Behaviors;
using the_office.infrastructure;

namespace the_office.api.Configurations;

public static class DependencyInjectionConfig
{
    public static void ResolveDependencies(this IServiceCollection services, ConfigureHostBuilder host)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        services.AddAutoMapper(AssemblyReference.Assembly);
            
        // Autofac configuration
        host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        host.ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new InfrastructureModule()); });
    }
}