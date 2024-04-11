namespace Authentication.Aplication.UseCase.Auth.Dto;

public record AuthenticationDto(Guid Id, string Name, string Email, string AccessToken);
