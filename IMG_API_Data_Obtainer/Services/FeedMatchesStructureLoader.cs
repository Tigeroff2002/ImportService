using ADF.Library.Models.Mapping.Feed.Description;
using ADF.Library.Models.Mapping.Internal.Description;
using IMG_API_Data_Obtainer.Logic.Abstractions;
using Microsoft.Extensions.Hosting;

namespace IMG_API_Data_Obtainer.Services;

public sealed class FeedMatchesStructureLoader : BackgroundService
{
    public FeedMatchesStructureLoader(
        IFeedEntitiesLoader<FeedSportDescription, IntSportDescription> feedSportsLoader,
        IFeedEntitiesLoader<FeedCategoryDescription, IntCategoryDescription> feedCategoriesLoader,
        IFeedEntitiesLoader<FeedChampionshipDescription, IntChampionshipDescription> feedChampionshipsLoader,
        IFeedEntitiesLoader<FeedTeamDescription, IntTeamDescription> feedTeamsLoader,
        IFeedEntitiesLoader<FeedEventDescription, IntEventDescription> feedEventsLoader)
    {
        _feedSportsLoader = feedSportsLoader ?? throw new ArgumentNullException(nameof(feedSportsLoader));
        _feedCategoriesLoader = feedCategoriesLoader ?? throw new ArgumentNullException(nameof(feedCategoriesLoader));
        _feedChampionshipsLoader = feedChampionshipsLoader ?? throw new ArgumentNullException(nameof(feedChampionshipsLoader));
        _feedTeamsLoader = feedTeamsLoader ?? throw new ArgumentNullException(nameof(feedTeamsLoader));
        _feedEventsLoader = feedEventsLoader ?? throw new ArgumentNullException(nameof(feedEventsLoader));
    }

    /// <inheritdoc/>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => Task.Run(async () =>
        {
            while (true)
            {
                await _feedSportsLoader.LoadAsync(stoppingToken)
                    .ConfigureAwait(false);

                await _feedCategoriesLoader.LoadAsync(stoppingToken)
                    .ConfigureAwait(false);

                await _feedChampionshipsLoader.LoadAsync(stoppingToken)
                    .ConfigureAwait(false);

                await _feedTeamsLoader.LoadAsync(stoppingToken)
                    .ConfigureAwait(false);

                await _feedEventsLoader.LoadAsync(stoppingToken)
                    .ConfigureAwait(false);
            }
        }, stoppingToken);

    private readonly IFeedEntitiesLoader<FeedSportDescription, IntSportDescription> _feedSportsLoader;
    private readonly IFeedEntitiesLoader<FeedCategoryDescription, IntCategoryDescription> _feedCategoriesLoader;
    private readonly IFeedEntitiesLoader<FeedChampionshipDescription, IntChampionshipDescription> _feedChampionshipsLoader;
    private readonly IFeedEntitiesLoader<FeedTeamDescription, IntTeamDescription> _feedTeamsLoader;
    private readonly IFeedEntitiesLoader<FeedEventDescription, IntEventDescription> _feedEventsLoader;
}
