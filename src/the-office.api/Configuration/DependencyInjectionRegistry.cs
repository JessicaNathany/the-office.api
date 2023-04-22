using the_office.insfrastructure.Data.Repository;
using the_office.insfrastructure.Data.Repository.Interface;

namespace the_office.api.Configuration
{
    public static class DependencyInjectionRegistry
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
