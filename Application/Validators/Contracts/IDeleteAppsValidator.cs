using Domain.Models;

namespace Application.Validators.Contracts
{
    public interface IDeleteAppsValidator
    {
        (bool, string) Validate(Applications? dbApp, string? sended);
    }
}
