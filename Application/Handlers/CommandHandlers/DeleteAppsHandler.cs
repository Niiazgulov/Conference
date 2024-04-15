using Application.Handlers.CommandHandlers.Results;
using Application.Handlers.Contracts.CommandHandlers;
using Application.Validators.Contracts;
using Domain.Models;
using Domain.ReadersContracts;
using Domain.RepositoryContracts;

namespace Application.Handlers.CommandHandlers
{
    public class DeleteAppsHandler : IDeleteAppsHandler
    {
        private IApplicationReader _applicationReader;
        private IApplicationRepository _applicationRepository;
        private IDeleteAppsValidator _deleteAppsValidator;
        public DeleteAppsHandler(IApplicationReader applicationReader, IApplicationRepository applicationRepository, IDeleteAppsValidator deleteAppsValidator)
        {
            _applicationReader = applicationReader;
            _applicationRepository = applicationRepository;
            _deleteAppsValidator = deleteAppsValidator;
        }

        public async Task<DeleteAppsResult> DeleteApps(Guid id)
        {
            Applications? dbApp = await _applicationReader.GetAppsById(id);
            var sended = await _applicationRepository.CheckSended(id);

            DeleteAppsResult deleteAppsResult = new();
            (deleteAppsResult.Result, deleteAppsResult.Message) = _deleteAppsValidator.Validate(dbApp,sended);
            if (deleteAppsResult.Result != true)
            {
                return deleteAppsResult;
            }

            await _applicationRepository.DeleteApps(id);

            return deleteAppsResult;
        }
    }
}
