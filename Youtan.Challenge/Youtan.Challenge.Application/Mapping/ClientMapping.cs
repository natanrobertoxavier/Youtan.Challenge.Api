using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.Mapping;

public static class ClientMapping
{
    public static Domain.Entities.Client ToEntity(this RequestRegisterClient request, string password)
    {
        return new Domain.Entities.Client(
            request.Name,
            request.Email.ToLower(),
            password
        );
    }
    public static ResponseLogin ToResponseLogin(this Domain.Entities.Client client, string token)
    {
        return new ResponseLogin(
            client.Name,
            client.Email,
            token
        );
    }
}
