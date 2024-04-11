using Authentication.Aplication.UseCase.Auth.Dto;

namespace Authentication.Aplication.UseCase.Auth.Commands.Register;

public record RegisterCommand(
    string Name,
    string LastName,
    string Email,
    string Password
) : IRequest<string>;
