using System.Runtime.Serialization;

namespace Youtan.Challenge.Exceptions.ExceptionBase;

[Serializable]
public class InvalidLoginException : YoutanException
{
    public InvalidLoginException() : base("Usuário ou senha inválidos")
    {
    }

    protected InvalidLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
