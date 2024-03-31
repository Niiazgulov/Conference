using Domain.Handlers.Contracts;
using Domain.Readers;

namespace Domain.Handlers
{
    public class GetUnsubmittedAppsHandler : IGetUnsubmittedAppsHandler
    {
        private IGetUnsubmittedAppsReader _getUnsubmittedAppsReader;
        public GetUnsubmittedAppsHandler(IGetUnsubmittedAppsReader getUnsubmittedAppsReader)
        {
            _getUnsubmittedAppsReader = getUnsubmittedAppsReader;
        }

        public Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime datetime)
        {
            Task<IEnumerable<Applications>> newapp = _getUnsubmittedAppsReader.GetUnsubmittedApps(datetime);

            return newapp;
        }
    }
}
