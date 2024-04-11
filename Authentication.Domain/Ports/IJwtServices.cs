
using Authentication.Domain.Entities;

namespace Authentication.Domain.Ports;

public interface IJwtServices
{
    Task<string> GenerateToken(User user);
}
