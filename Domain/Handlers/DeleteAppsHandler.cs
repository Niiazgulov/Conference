using Domain.Handlers.Contracts;
using Domain.RepositoryContracts;

namespace Domain.Handlers
{
    public class DeleteAppsHandler : IDeleteAppsHandler
    {
        private IDeleteAppsRepository _deleteAppsRepository;
        public DeleteAppsHandler(IDeleteAppsRepository deleteAppsRepository)
        {
            _deleteAppsRepository = deleteAppsRepository;
        }

        public Task DeleteApps(Guid id)
        {
            Task deleted = _deleteAppsRepository.DeleteApps(id);

            return deleted;
        }
    }
}
