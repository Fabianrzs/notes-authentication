using System.Runtime.Serialization;

namespace Authentication.Domain.Exceptions;

public class IncorrectCredentialsException : Reservar.Common.Domain.Exceptions.AppException
{
    public IncorrectCredentialsException(string message) : base(message)
    {
    }

    public IncorrectCredentialsException(string message, Exception inner) : base(message, inner)
    {
    }

    protected IncorrectCredentialsException(SerializationInfo info, StreamingContext content) : base(info, content)
    {
    }
}

