using Application.Validators.Contracts;
using Domain.Models;

namespace Application.Validators
{
    public class GetSubOrUnsubValidator : IGetSubOrUnsubValidator
    {
        public (bool, bool, DateTime?, string?) Validate(SubOrUnsubDTO req)
        {
            if (req.submittedAfter == null && req.unsubmittedOlder == null)
            {
                return (false, false, null, "Укажите один из параметров (submittedAfter или unsubmittedOlder).");
            }
            if (req.submittedAfter != null && req.unsubmittedOlder != null)
            {
                return (false, false, null, "Укажите только один из параметров (submittedAfter или unsubmittedOlder).");
            }
            if (req.submittedAfter != null)
            {
                return (true, true, req.submittedAfter, String.Empty);
            }
            if (req.unsubmittedOlder != null)
            {
                return (true, false, req.unsubmittedOlder, String.Empty);
            }
            return (false, false, null, "Ошибка при валидации");
        }
    }
}
