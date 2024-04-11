namespace Authentication.Aplication.UseCase.Auth.Commands.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(_ => _.Email).NotNull().MinimumLength(4).MaximumLength(50);
        RuleFor(_ => _.Password).NotNull().NotEmpty().MinimumLength(8).MaximumLength(50);
        RuleFor(_ => _.Name).NotNull().NotEmpty();
        RuleFor(_ => _.LastName).NotNull().NotEmpty();
    }
}
