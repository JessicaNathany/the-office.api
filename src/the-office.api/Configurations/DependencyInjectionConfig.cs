using the_office.domain.Interface;
using the_office.infrastructure.Data.Repositories;

namespace the_office.api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            services.AddScoped<IPhrasesRepository, PhrasesRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
        }
    }
}
