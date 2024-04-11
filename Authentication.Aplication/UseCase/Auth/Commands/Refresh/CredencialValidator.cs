namespace Authentication.Aplication.UseCase.Auth.Commands.Credential;

public class RefreshValidator : AbstractValidator<RefreshCommand>
{
    public RefreshValidator()
    {
        RuleFor(_ => _.Id).NotNull();

    }
}
