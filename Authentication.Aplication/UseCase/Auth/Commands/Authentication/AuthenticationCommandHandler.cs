using Authentication.Aplication.UseCase.Auth.Dto;
using Authentication.Application.UseCase.Auth.Commands;
using Authentication.Domain.Ports;
using Authentication.Domain.Services;
using Common.Communication.Publisher.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Aplication.UseCase.Auth.Commands.Authentication;

public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, AuthenticationDto>
{
    private readonly IIntegrationMessagePublisher _messagePublisher;
    private readonly AuthenticationService _authenticationService;
    private readonly IJwtServices _jwtService;


    public AuthenticationCommandHandler(AuthenticationService authenticationService, IJwtServices jwtService, IIntegrationMessagePublisher messagePublisher)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        _jwtService = jwtService;
    }

    public async Task<AuthenticationDto> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.ValidateUserCredentials(request.Email, request.Password);
        var tokenGenerated = await _jwtService.GenerateToken(user);
        var auth = new AuthenticationDto(user.Id, user.Name, user.Email, tokenGenerated);
        var email = new EmailCommand(user.Email, "Nuevo Inicio de Session ", $"Se a detectado un nuevo inicio de sesion el aplicacion de notas {DateTime.Now}", null);
        await _messagePublisher.Publish(email);
        return auth;
    }
}