using Domain.Models;

namespace Application.Validators.Contracts
{
    public interface IGetSubOrUnsubValidator
    {
        (bool, bool, DateTime?, string?) Validate(SubOrUnsubDTO req);
    }
}
