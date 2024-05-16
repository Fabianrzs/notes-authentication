using Authentication.Domain.Entities;
using Authentication.Domain.Services;
using Common.Communication.Publisher.Integration;
using Reservar.Common.Domain.Entities.Subscription;

namespace Authentication.Aplication.UseCase.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IIntegrationMessagePublisher _messagePublisher;
    private readonly AuthenticationService _authenticationService;
    private readonly IMapper _mapper;


    public RegisterCommandHandler(AuthenticationService authenticationService, IMapper mapper, IIntegrationMessagePublisher messagePublisher)
    {
        _authenticationService =
            authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        _mapper = mapper;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.RegisterUserCredentials(_mapper.Map<User>(request));

        var email = new EmailCommand(request.Email, "Registro de Usuario ", $"En buena hora te haz registrado en app de notas, Ya puedes seguir con tu inicio de session", null);
        await _messagePublisher.Publish(email);
        return $"El usuario {user.Name} fue registrado con exito";
    }
}