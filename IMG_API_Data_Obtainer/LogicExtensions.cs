using Microsoft.Extensions.DependencyInjection;

using IMG_API_Data_Obtainer.Logic.Abstractions;
using IMG_API_Data_Obtainer.Logic;
using IMG_API_Data_Obtainer.Services;

using IMG_API_Data_Obtainer.Models;
using IMG_API_Data_Obtainer.Models.Feed;
using IMG_API_Data_Obtainer.Models.Internal;

public static class LogicExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
        => services
            .AddSingleton<IEntitiesStructureDeserializer, EntitiesStructureDeserializer>()
            .AddSingleton<IFeedEntitiesRepository, FeedEntitiesRepository>()
            .AddDependenciesForFeed<FeedSportDescription, IntSportDescription>()
            .AddDependenciesForFeed<FeedCategoryDescription, IntCategoryDescription>()
            .AddDependenciesForFeed<FeedChampionshipDescription, IntChampionshipDescription>()
            .AddDependenciesForFeed<FeedPlayerDescription, IntPlayerDescription>()
            .AddDependenciesForFeed<FeedEventDescription, IntEventDescription>();

    public static IServiceCollection AddMyHostedService(this IServiceCollection services)
        => services.AddHostedService<FeedMatchesStructureLoader>();


    private static IServiceCollection AddDependenciesForFeed<TFeed, TInternal>(this IServiceCollection services)
        where TFeed : MappableEntity<TInternal>
        where TInternal : IInternalEntityMarker
            => services.AddSingleton<
                IFeedEntitiesLoader<TFeed, TInternal>,
                FeedEntitiesLoader<TFeed, TInternal>>();
}