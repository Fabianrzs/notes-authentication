using Authentication.Aplication.UseCase.Auth.Dto;

namespace Authentication.Aplication.UseCase.Auth.Commands.Credential;

public record CredencialCommand(
    string accessToken
) : IRequest<AuthenticationDto>;
