using Domain.Handlers.Contract;
using Domain.Readers;
using Domain.Repository;

namespace Domain.Handlers
{
    public class ReaderHandler : IReaderHandler
    {
        private IConferenceAppsReader _conferenceAppsReader;
        public ReaderHandler(IConferenceAppsReader conferenceAppsReader)
        {
            _conferenceAppsReader = conferenceAppsReader;
        }

        public Task<Applications> GetAppsById(Guid id)
        {
            Task<Applications> app = _conferenceAppsReader.GetAppsById(id);

            return app;
        }
        public Task<Applications> GetAppByAuthorId(Guid id)
        {
            Task<Applications> app = _conferenceAppsReader.GetAppByAuthorId(id);

            return app;
        }

        public Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime datetime)
        {
            Task<IEnumerable<Applications>> newapp = _conferenceAppsReader.GetUnsubmittedApps(datetime);

            return newapp;
        }


        public Task<IEnumerable<Applications>> GetSubmittedApps(DateTime datetime)
        {
            Task<IEnumerable<Applications>> newapp = _conferenceAppsReader.GetSubmittedApps(datetime);

            return newapp;
        }

        
        public Activities[] _activities =
        {
            new Activities(){Activity = "Report", Description = "Доклад, 35-45 минут" },
            new Activities(){Activity = "Masterclass", Description = "Мастеркласс, 1-2 часа" },
            new Activities(){Activity = "Discussion", Description = "Дискуссия / круглый стол, 40-50 минут" },
        };

        public Activities[] GetActivities()
        {
            return _activities;
        }
    }
}
