using Microsoft.Extensions.DependencyInjection;

using IMG_API_Data_Obtainer.Logic.Abstractions;
using IMG_API_Data_Obtainer.EntitiesModels;
using IMG_API_Data_Obtainer.Logic;
using IMG_API_Data_Obtainer.Services;

using IMG_API_Data_Obtainer.Models;
using IMG_API_Data_Obtainer.Models.Feed;
using IMG_API_Data_Obtainer.Models.Internal;
using ADF.Library.Common.Logic.Serialization;
using IMG_API_Data_Obtainer.Logic.Deserializers;

public static class LogicExtensions
{
    public static IServiceCollection AddImportLogic(this IServiceCollection services)
        => services
            .AddMyHostedService()
            .AddDependenciesForFeed<FeedSportDescription, IntSportDescription>()
            .AddDependenciesForFeed<FeedCategoryDescription, IntCategoryDescription>()
            .AddDependenciesForFeed<FeedChampionshipDescription, IntChampionshipDescription>()
            .AddDependenciesForFeed<FeedTeamDescription, IntTeamDescription>()
            .AddDependenciesForFeed<FeedEventDescription, IntEventDescription>()
            .AddSingleton<IFeedEntitiesRepository, FeedEntitiesRepository>()
            .AddSingleton<IEntitiesStructureFetcher, EntitiesStructureFetcher>()
            .AddDeserializers();

    public static IServiceCollection AddMyHostedService(this IServiceCollection services)
        => services.AddHostedService<FeedMatchesStructureLoader>();


    private static IServiceCollection AddDependenciesForFeed<TFeed, TInternal>(this IServiceCollection services)
        where TFeed : MappableEntity<TInternal>
        where TInternal : IInternalEntityMarker
            => services.AddSingleton<
                IFeedEntitiesLoader<TFeed, TInternal>,
                FeedEntitiesLoader<TFeed, TInternal>>();

    private static IServiceCollection AddDeserializers(this IServiceCollection services)
        => services
            .AddSingleton<IDeserializer<string, IReadOnlyDictionary<Id<Sport>, Sport>>, SportsDeserializer>()
            .AddSingleton<IDeserializer<string, IReadOnlyList<Country>>, CountriesDeserializer>()
            .AddSingleton<IDeserializer<string, IReadOnlyList<Tournament>>, TournamentsDeserializer>()
            .AddSingleton<IDeserializer<string, IReadOnlyList<Competition>>, CompetitionsDeserializer>()
            .AddSingleton<IDeserializer<string, IReadOnlyList<Team>>, TeamsDeserializer>()
            .AddSingleton<IDeserializer<string, IReadOnlyList<Match>>, MatchesDeserializer>()
            .AddSingleton<IDeserializer<string, IReadOnlyList<Name<Match>>>, DeletedMatchesDeserializer>();
}