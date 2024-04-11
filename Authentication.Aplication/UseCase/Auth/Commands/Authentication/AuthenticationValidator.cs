namespace Authentication.Aplication.UseCase.Auth.Commands.Authentication;

public class AuthenticationValidator : AbstractValidator<AuthenticationCommand>
{
    public AuthenticationValidator()
    {
        RuleFor(_ => _.Email).NotNull().MinimumLength(4).MaximumLength(50);
        RuleFor(_ => _.Password).NotNull().NotEmpty().MinimumLength(8).MaximumLength(50);

    }
}
