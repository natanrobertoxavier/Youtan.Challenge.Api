using System.Runtime.Serialization;

namespace Youtan.Challenge.Exceptions.ExceptionBase;

public class YoutanException : SystemException
{
    public YoutanException(string mensagem) : base(mensagem)
    {
    }

    protected YoutanException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}