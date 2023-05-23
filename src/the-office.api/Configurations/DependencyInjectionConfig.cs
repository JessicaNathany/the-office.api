using Autofac;
using Autofac.Extensions.DependencyInjection;
using the_office.infrastructure;

namespace the_office.api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services, ConfigureHostBuilder host)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(application.Common.AssemblyReference.Assembly);
            
                // TODO: For future implementation
                // Mediator Behaviors
                // config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                // config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            
            services.AddAutoMapper(application.Common.AssemblyReference.Assembly);
            
            // Autofac configuration
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new InfrastructureModule()); });
        }
    }
}