using Application.Handlers.CommandHandlers.Results;
using Application.Handlers.Contracts.CommandHandlers;
using Application.Validators.Contracts;
using Domain.Models;
using Domain.ReadersContracts;
using Domain.RepositoryContracts;

namespace Application.Handlers.CommandHandlers
{
    public class EditAppsHandler : IEditAppsHandler
    {
        private IApplicationReader _applicationReader;
        private IApplicationRepository _applicationRepository;
        private IEditAppsValidator _editAppsValidator;
        public EditAppsHandler(IApplicationReader applicationReader, IApplicationRepository applicationRepository, IEditAppsValidator editAppsValidator)
        {
            _applicationReader = applicationReader;
            _applicationRepository = applicationRepository;
            _editAppsValidator = editAppsValidator;
        }

        public async Task<EditAppsResult> EditApps(Guid id, EditedAppDTO app)
        {
            Applications? dbApp = await _applicationReader.GetAppsById(id);
            var sended = await _applicationRepository.CheckSended(id);

            EditAppsResult editAppsResult = new();
            (editAppsResult.Result, editAppsResult.Message) = _editAppsValidator.Validate(app, dbApp, sended);
            if (editAppsResult.Result != true)
            {
                return editAppsResult;
            }

            editAppsResult.Editedapp = await _applicationRepository.EditApps(id, app);
            return editAppsResult;
        }
    }
}
