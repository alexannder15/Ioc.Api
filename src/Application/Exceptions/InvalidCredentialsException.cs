using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string message)
        : base(message) { }

    public InvalidCredentialsException(string message, Exception innerException)
        : base(message, innerException) { }

    public InvalidCredentialsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
