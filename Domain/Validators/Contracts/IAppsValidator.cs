namespace Domain.Validators.Contracts
{
    public interface IAppsValidator
    {
        (bool, string) Validate(NewAppDTO app);
    }
}
