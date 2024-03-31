using Domain.Handlers.Contracts;
using Domain.RepositoryContracts;

namespace Domain.Handlers
{
    public class CheckSendedHandler : ICheckSendedHandler
    {
        private ICheckSendedRepository _checkSendedRepository;
        public CheckSendedHandler(ICheckSendedRepository checkSendedRepository)
        {
            _checkSendedRepository = checkSendedRepository;
        }

        public Task<string> CheckSended(Guid id)
        {
            Task<string> checkresult = _checkSendedRepository.CheckSended(id);
            return checkresult;
        }
    }
}
