using System.Runtime.Serialization;

namespace Youtan.Challenge.Exceptions.ExceptionBase;

[Serializable]
public class ValidationErrorsException : YoutanException
{
    public List<string> ErrorMessages { get; set; } = [];
    public ValidationErrorsException(List<string> errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ValidationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
