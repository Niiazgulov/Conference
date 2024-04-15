using Application.Handlers.CommandHandlers.Results;

namespace Application.Handlers.Contracts.CommandHandlers
{
    public interface IAddAppsToReviewHandler
    {
        Task<ReviewAddAppsResult> AddAppsToReview(Guid id);
    }
}
