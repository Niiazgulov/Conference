using Domain.Handlers.Contracts;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class RepositoryHandler : IRepositoryHandler
    {
        private IConferenceAppsRepository _conferenceAppsRepository;
        public RepositoryHandler(IConferenceAppsRepository conferenceAppsRepository)
        {
            _conferenceAppsRepository = conferenceAppsRepository;      
        }

        public Task<Applications> AddApps(NewAppDTO app)
        {
            Task<Applications> newapp = _conferenceAppsRepository.AddApps(app);

            return newapp;
        }

        public Task<Applications> EditApps(Guid id, EditedAppDTO app)
        {
            Task<Applications> editedapp = _conferenceAppsRepository.EditApps(id, app);

            return editedapp;
        }
        public Task DeleteApps(Guid id)
        {
            Task deleted = _conferenceAppsRepository.DeleteApps(id);

            return deleted;
        }

}
}
