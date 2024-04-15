using Application.Handlers.CommandHandlers.Results;
using Application.Handlers.Contracts.CommandHandlers;
using Application.Validators.Contracts;
using Domain.Models;
using Domain.ReadersContracts;
using Domain.RepositoryContracts;

namespace Application.Handlers.CommandHandlers
{
    public class AddAppsToReviewHandler : IAddAppsToReviewHandler
    {
        private IApplicationReader _applicationReader;
        private IApplicationRepository _applicationRepository;
        private IReviewAddAppsValidator _reviewAddAppsValidator;

        public AddAppsToReviewHandler(IApplicationReader applicationReader, IApplicationRepository applicationRepository, IReviewAddAppsValidator reviewAddAppsValidator)
        {
            _applicationReader = applicationReader;
            _applicationRepository = applicationRepository;
            _reviewAddAppsValidator = reviewAddAppsValidator;
        }

        public async Task<ReviewAddAppsResult> AddAppsToReview(Guid id)
        {
            Applications? dbApp = await _applicationReader.GetAppsById(id);
            var sended = await _applicationRepository.CheckSended(id);

            ReviewAddAppsResult reviewAppsResult = new();
            (reviewAppsResult.Result, reviewAppsResult.Message) = _reviewAddAppsValidator.Validate(dbApp, sended);
            if (reviewAppsResult.Result != true)
            {
                return reviewAppsResult;
            }

            (reviewAppsResult.Result, reviewAppsResult.Message) = await _applicationRepository.AddAppsToReview(id);
                
            if (reviewAppsResult.Result)
                    return reviewAppsResult;
            
            return reviewAppsResult;
        }
    }
}