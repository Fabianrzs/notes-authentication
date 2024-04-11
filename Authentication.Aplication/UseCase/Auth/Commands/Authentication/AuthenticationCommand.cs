using Authentication.Aplication.UseCase.Auth.Dto;

namespace Authentication.Aplication.UseCase.Auth.Commands.Authentication;

public record AuthenticationCommand(
    string Email,
    string Password
) : IRequest<AuthenticationDto>;
