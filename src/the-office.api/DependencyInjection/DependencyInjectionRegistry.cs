using the_office.insfrastructure.Repository;
using the_office.insfrastructure.Repository.Interface;

namespace the_office.api.Configurations
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
