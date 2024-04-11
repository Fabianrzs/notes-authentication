using static Google.Apis.Auth.GoogleJsonWebSignature;
using Authentication.Domain.Exceptions;
using Authentication.Domain.Entities;
using Authentication.Domain.Ports;
using Google.Apis.Auth;

namespace Authentication.Domain.Services;
public class AuthenticationService
{
    private readonly IGenericRepository<User> _repository;

    public AuthenticationService(IGenericRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User> RefreshUserCredentials(Guid Id)
    {
        var user = (await _repository.GetByIdAsync(Id));
        if (user == null)
        {
            throw new FailCredentialsException("Fail Refresh token");
        }
        return user;
    }

    public async Task<User> ValidateUserCredentials(string email, string password)
    {
        var user = (await _repository.GetAsync(u => u.Email == email && u.Password == password)).FirstOrDefault();
        if (user == null)
        {
            throw new IncorrectCredentialsException("Email And Password Incorrects");
        }
        return user;
    }

    public async Task<User> RegisterUserCredentials(User register)
    {
        var user = new User();
        var userFind = (await _repository.GetAsync(u => u.Email == register.Email)).FirstOrDefault();
        if (userFind != null) {
            throw new DuplicateCredentialsException("Existing email");
        }
            user = (await _repository.AddAsync(register));
        return user;
    }

    public async Task<User> AuthenticateUserCredentials(string accessToken)
    {
        var findUser = await ValidateAccessTokenGoogleAsync(accessToken);
        var user = (await _repository.GetAsync(u => u.Email == findUser.Email)).FirstOrDefault();
        if (user == null)
        {
            user = new User { 
                Email = findUser.Email,
                Password = "",
                Name = findUser.Name,
                LastName = findUser.FamilyName
            };
            user = (await _repository.AddAsync(user));
        }
        return user;
    }

    private async Task<Payload> ValidateAccessTokenGoogleAsync(string accessToken)
    {
        Payload payload = await GoogleJsonWebSignature.ValidateAsync(accessToken);
        if (payload == null)
        {
            throw new IncorrectCredentialsException("Invalid Credencials");
        }
        return payload;
    }

}

