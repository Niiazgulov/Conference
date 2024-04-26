using Application.Validators.Contracts;
using Domain.Models;

namespace Application.Validators
{
    public class ReviewAddAppsValidator : IReviewAddAppsValidator
    {
        public (bool, string) Validate(Applications? dbApp, string? sended)
        {
            if (dbApp == null )
                return (false, "ОШИБКА! Такой заявки не существует!");

            if (sended == "YES")
                return (false, "ОШИБКА! Невозможно выполнить, заявка уже направлена на рассмотрение.");

            return (true, String.Empty);
        }
    }
}