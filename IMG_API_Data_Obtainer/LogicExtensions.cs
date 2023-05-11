using Microsoft.Extensions.DependencyInjection;

using ADF.Library.Models.Mapping;

using IMG_API_Data_Obtainer.Logic.Abstractions;
using IMG_API_Data_Obtainer.Logic;
using IMG_API_Data_Obtainer.Services;

using ADF.Library.Models.Mapping.Feed.Description;
using ADF.Library.Models.Mapping.Internal.Description;

public static class LogicExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
        => services
            .AddSingleton<IEntitiesStructureDeserializer, EntitiesStructureDeserializer>()
            .AddSingleton<IFeedEntitiesRepository, FeedEntitiesRepository>()
            .AddDependenciesForFeed<FeedSportDescription, IntSportDescription>()
            .AddDependenciesForFeed<FeedCategoryDescription, IntCategoryDescription>()
            .AddDependenciesForFeed<FeedChampionshipDescription, IntChampionshipDescription>()
            .AddDependenciesForFeed<FeedTeamDescription, IntTeamDescription>()
            .AddDependenciesForFeed<FeedEventDescription, IntEventDescription>();

    public static IServiceCollection AddMyHostedService(this IServiceCollection services)
        => services.AddHostedService<FeedMatchesStructureLoader>();


    private static IServiceCollection AddDependenciesForFeed<TFeed, TInternal>(this IServiceCollection services)
        where TFeed : MappableEntity<TInternal>
            => services.AddSingleton<IFeedEntitiesLoader<TFeed, TInternal>, FeedEntitiesLoader<TFeed, TInternal>>();
}