using Application.Handlers.CommandHandlers.Results;
using Application.Handlers.Contracts.CommandHandlers;
using Application.Validators.Contracts;
using Domain.Models;
using Domain.RepositoryContracts;

namespace Application.Handlers.CommandHandlers
{
    public class AddNewApplicationHandler : IAddNewApplicationHandler
    {
        private IApplicationRepository _applicationRepository;
        private IAddAppsValidator _appsValidator;

        public AddNewApplicationHandler(IApplicationRepository applicationRepository, IAddAppsValidator appsValidator)
        {
            _applicationRepository = applicationRepository;
            _appsValidator = appsValidator;
        }

        public async Task<AddAppsResult> AddApps(NewAppDTO app)
        {
            var exists = await _applicationRepository.CheckUserById(app.Author);
            AddAppsResult addAppsResult = new();
            (addAppsResult.Result, addAppsResult.Message) = _appsValidator.Validate(app, exists);
            if (addAppsResult.Result != true)
            {
                return addAppsResult;
            }

            addAppsResult.Newapp = await _applicationRepository.AddApps(app);

            return addAppsResult;
        }
    }
}
