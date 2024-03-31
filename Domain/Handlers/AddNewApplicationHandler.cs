using Domain.Handlers.Contracts;
using Domain.Repository;

namespace Domain.Handlers
{
    public class AddNewApplicationHandler : IAddNewApplicationHandler
    {
        private IAddNewApplicationRepository _addNewApplicationRepository;
        private ICheckUserByIdRepository _checkUserByIdRepository;

        public AddNewApplicationHandler(IAddNewApplicationRepository addNewApplicationRepository, ICheckUserByIdRepository checkUserByIdRepository)
        {
            _addNewApplicationRepository = addNewApplicationRepository;
            _checkUserByIdRepository = checkUserByIdRepository;
        }

        public async Task<Applications> AddApps(NewAppDTO app)
        {
            var emptyApp = new Applications();
            var dbCount = NonNullPropertiesCount(app);
            if (dbCount > 1)
            {
                var exists = await _checkUserByIdRepository.CheckUserById(app.Author);

                if (exists == true)
                {
                    emptyApp.Author = new Guid("00000000-0000-0000-0000-000000000001");
                    return emptyApp;
                }

                var newapp = await _addNewApplicationRepository.AddApps(app);
                return newapp;
            }

            return emptyApp;
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
