using Authentication.Aplication.UseCase.Auth.Dto;
using Authentication.Domain.Ports;
using Authentication.Domain.Services;
using Common.Communication.Publisher.Integration;
using Reservar.Common.Domain.Entities.Subscription;

namespace Authentication.Aplication.UseCase.Auth.Commands.Credential;

public class CredencialCommandHandler : IRequestHandler<CredencialCommand, AuthenticationDto>
{
    private readonly IIntegrationMessagePublisher _messagePublisher;
    private readonly AuthenticationService _authenticationService;
    private readonly IJwtServices _jwtService;


    public CredencialCommandHandler(AuthenticationService authenticationService, IJwtServices jwtService, IIntegrationMessagePublisher messagePublisher)
    {
        _authenticationService =
            authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        _jwtService = jwtService;
    }

    public async Task<AuthenticationDto> Handle(CredencialCommand request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.AuthenticateUserCredentials(request.accessToken);
        var tokenGenerated = await _jwtService.GenerateToken(user);
        var auth = new AuthenticationDto(user.Id, user.Name, user.Email, tokenGenerated);
        var email = new EmailCommand(user.Email, "Nuevo Inicio de Session ", $"Se a detectado un nuevo inicio de sesion el aplicacion de notas {DateTime.Now}", null);
        await _messagePublisher.Publish(email);
        return auth;
    }
}