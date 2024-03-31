namespace Domain.RepositoryContracts
{
    public interface IAddAppsToReviewRepository
    {
        Task<string> AddAppsToReview(Guid id);
    }
}
