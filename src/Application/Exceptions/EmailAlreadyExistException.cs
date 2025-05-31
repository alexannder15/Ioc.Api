using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class EmailAlreadyExistException : Exception
{
    public EmailAlreadyExistException(string message)
        : base(message) { }

    public EmailAlreadyExistException(string message, Exception innerException)
        : base(message, innerException) { }

    public EmailAlreadyExistException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
