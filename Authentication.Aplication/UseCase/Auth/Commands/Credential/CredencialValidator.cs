namespace Authentication.Aplication.UseCase.Auth.Commands.Credential;

public class CredencialValidator : AbstractValidator<CredencialCommand>
{
    public CredencialValidator()
    {
        RuleFor(_ => _.accessToken).NotNull();

    }
}
