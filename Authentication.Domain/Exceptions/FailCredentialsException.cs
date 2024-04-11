using System.Runtime.Serialization;

namespace Authentication.Domain.Exceptions;

public class FailCredentialsException : Reservar.Common.Domain.Exceptions.AppException
{
    public FailCredentialsException(string message) : base(message)
    {
    }

    public FailCredentialsException(string message, Exception inner) : base(message, inner)
    {
    }

    protected FailCredentialsException(SerializationInfo info, StreamingContext content) : base(info, content)
    {
    }
}

