﻿namespace Youtan.Challenge.Communication.Reponse;

public class ResponseError
{
    public List<string> Messages { get; set; }
    public ResponseError(string messages)
    {
        Messages = new List<string>
    {
        messages
    };
    }

    public ResponseError(List<string> messages)
    {
        Messages = messages;
    }
}
