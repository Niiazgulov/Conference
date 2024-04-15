using Domain.Models;

namespace Application.Validators.Contracts
{
    public interface IAddAppsValidator
    {
        (bool, string) Validate(NewAppDTO app, bool exists);
    }
}
