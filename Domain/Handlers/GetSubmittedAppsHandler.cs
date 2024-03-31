using Domain.Handlers.Contract;
using Domain.Readers;

namespace Domain.Handlers
{
    public class GetSubmittedAppsHandler : IGetSubmittedAppsHandler
    {
        private IGetSubmittedAppsReader _getSubmittedAppsReader;
        public GetSubmittedAppsHandler(IGetSubmittedAppsReader getSubmittedAppsReader)
        {
            _getSubmittedAppsReader = getSubmittedAppsReader;
        }

        public Task<IEnumerable<Applications>> GetSubmittedApps(DateTime datetime)
        {
            Task<IEnumerable<Applications>> newapp = _getSubmittedAppsReader.GetSubmittedApps(datetime);

            return newapp;
        }

    }
}
