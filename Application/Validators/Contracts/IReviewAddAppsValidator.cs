using Domain.Models;

namespace Application.Validators.Contracts
{
    public interface IReviewAddAppsValidator
    {
        (bool, string) Validate(Applications? dbApp, string? sended);
    }
}
