using Reservar.Common.Domain.Exceptions;
using System.Runtime.Serialization;

namespace Authentication.Domain.Exceptions;

public class DuplicateCredentialsException: AppException
{
    public DuplicateCredentialsException(string message) : base(message)
    {
    }

    public DuplicateCredentialsException(string message, Exception inner) : base(message, inner)
    {
    }

    protected DuplicateCredentialsException(SerializationInfo info, StreamingContext content) : base(info, content)
    {
    }
}

