using the_office.domain.Interface;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Repositories;

namespace the_office.api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
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
            
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            services.AddScoped<IPhrasesRepository, PhrasesRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
        }
    }
}