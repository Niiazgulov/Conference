using Domain.Models;

namespace Application.Validators.Contracts
{
    public interface IEditAppsValidator
    {
        (bool, string) Validate(EditedAppDTO app, Applications? dbApp, string? sended);
    }
}
