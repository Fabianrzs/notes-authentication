using Authentication.Aplication.UseCase.Auth.Dto;
using Authentication.Domain.Ports;
using Authentication.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Aplication.UseCase.Auth.Commands.Credential;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, string>
{
    private readonly AuthenticationService _authenticationService;
    private readonly IJwtServices _jwtService;


    public RefreshCommandHandler(AuthenticationService authenticationService, IJwtServices jwtService)
    {
        _authenticationService =
            authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _jwtService = jwtService;
    }

    public async Task<string> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.RefreshUserCredentials(request.Id);
        var tokenGenerated = await _jwtService.GenerateToken(user);
        return (await _jwtService.GenerateToken(user));
    }
}