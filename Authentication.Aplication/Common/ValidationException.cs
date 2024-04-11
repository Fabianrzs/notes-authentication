using Reservar.Common.Domain.Response;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Authentication.Aplication.Common;

public class ValidationException : Exception
{
    public List<string> Errors { get; }

    public ValidationException(IEnumerable<string> failures) 
    {
        Errors = failures.ToList();
    }

    public async Task HandleError(HttpContext context, Response<string> response)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
}
