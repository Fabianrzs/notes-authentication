using Authentication.Aplication.UseCase.Auth.Dto;

namespace Authentication.Aplication.UseCase.Auth.Commands.Credential;

public record RefreshCommand(
    Guid Id
) : IRequest<string>;
