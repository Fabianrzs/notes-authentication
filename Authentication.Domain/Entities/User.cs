using Reservar.Common.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Authentication.Domain.Entities;

public class User: EntityBase<Guid>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string? Password { get; set; }
}

