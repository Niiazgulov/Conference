namespace Domain.Handlers.Contracts
{
    public interface IAddAppsToReviewHandler
    {
        Task<string> AddAppsToReview(Guid id);
    }
}
