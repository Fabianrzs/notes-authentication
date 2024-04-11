
namespace Authentication.Application.UseCase.Auth.Commands;
public record EmailCommand(
    string To,
    string Subject,
    string Body,
    List<string>? Attachments
);