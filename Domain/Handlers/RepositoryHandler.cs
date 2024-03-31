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
            Task<Applications> emptyApp = _conferenceAppsRepository.EmptyApp();
            var dbCount = NonNullPropertiesCount(app);
            if (dbCount > 1)
            {
                Task<bool> exists = _conferenceAppsRepository.CheckUserById(app.Author);

                if (exists.Result == true)
                {
                    emptyApp.Result.Author = new Guid("00000000-0000-0000-0000-000000000001");
                    return emptyApp;
                }

                Task<Applications> newapp = _conferenceAppsRepository.AddApps(app);
                return newapp;
            }
            
            return emptyApp;
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

        public Task<string> AddAppsToReview(Guid id)
        {
            Task<string> added = _conferenceAppsRepository.AddAppsToReview(id);
            return added;
        }

        public Task<string> CheckSended(Guid id)
        {
            Task<string> checkresult = _conferenceAppsRepository.CheckSended(id);
            return checkresult;
        }


        public int NonNullPropertiesCount(object entity)
        {
            return entity.GetType()
                         .GetProperties()
                         .Select(x => x.GetValue(entity, null))
                         .Count(v => v != null);
        }

    }
}
