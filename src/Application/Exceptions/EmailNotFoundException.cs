using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class EmailNotFoundException : Exception
{
    public EmailNotFoundException(string message)
        : base(message) { }

    public EmailNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }

    public EmailNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
