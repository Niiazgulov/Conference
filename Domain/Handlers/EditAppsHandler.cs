using Domain.Handlers.Contracts;
using Domain.RepositoryContracts;

namespace Domain.Handlers
{
    public class EditAppsHandler : IEditAppsHandler
    {
        private IEditAppsRepository _editAppsRepository;
        public EditAppsHandler(IEditAppsRepository editAppsRepository)
        {
            _editAppsRepository = editAppsRepository;      
        }

        public Task<Applications?> EditApps(Guid id, EditedAppDTO app)
        {
            Task<Applications?> editedapp = _editAppsRepository.EditApps(id, app);

            return editedapp;
        }

    }
}
