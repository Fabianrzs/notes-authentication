using Authentication.Aplication.UseCase.Auth.Commands.Register;
using Authentication.Domain.Entities;

namespace Authentication.Aplication.UseCase.Auth;

public class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterCommand, User>()
                .ReverseMap();
    }
}
