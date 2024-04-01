using Domain.Handlers.Contracts;
using Domain.Repository;
using Domain.Validators;
using Domain.Validators.Contracts;

namespace Domain.Handlers
{
    public class AddNewApplicationHandler : IAddNewApplicationHandler
    {
        private IAddNewApplicationRepository _addNewApplicationRepository;
        private ICheckUserByIdRepository _checkUserByIdRepository;
        private IAppsValidator _appsValidator;

        public AddNewApplicationHandler(IAddNewApplicationRepository addNewApplicationRepository, ICheckUserByIdRepository checkUserByIdRepository, IAppsValidator appsValidator)
        {
            _addNewApplicationRepository = addNewApplicationRepository;
            _checkUserByIdRepository = checkUserByIdRepository;
            _appsValidator = appsValidator;
        }

        public async Task<(bool, string, Applications)> AddApps(NewAppDTO app)
        {
            var emptyApp = new Applications();

            (bool res, string message) = _appsValidator.Validate(app);
            if (res != true)
            {
                return (false, message, emptyApp);
            }

            var dbCount = NonNullPropertiesCount(app);

            if (dbCount > 1)
            {
                var exists = await _checkUserByIdRepository.CheckUserById(app.Author);

                if (exists == true)
                {
                    string message2 = "У этого автора уже есть 1 заявка в черновиках, добавить новую невозможно!";
                    return (false, message2, emptyApp);
                }

                var newapp = await _addNewApplicationRepository.AddApps(app);
                return (true, "", newapp);
            }

            string message3 = "Добавьте как минимум 1 поле в заявку кроме Id автора!";
            return (false, message3, emptyApp);
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
