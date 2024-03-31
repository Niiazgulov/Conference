using Domain.Handlers.Contracts;
using Domain.RepositoryContracts;

namespace Domain.Handlers
{
    public class AddAppsToReviewHandler : IAddAppsToReviewHandler
    {
        private IAddAppsToReviewRepository _addAppsToReviewRepository;
        public AddAppsToReviewHandler(IAddAppsToReviewRepository addAppsToReviewRepository)
        {
            _addAppsToReviewRepository = addAppsToReviewRepository;
        }

        public Task<string> AddAppsToReview(Guid id)
        {
            Task<string> added = _addAppsToReviewRepository.AddAppsToReview(id);
            return added;
        }
    }
}