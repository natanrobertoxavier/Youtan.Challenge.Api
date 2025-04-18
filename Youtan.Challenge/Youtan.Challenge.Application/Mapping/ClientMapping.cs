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

    public static ResponseClient ToResponse(this Domain.Entities.Client client)
    {
        return new ResponseClient(
            client.Id,
            client.RegistrationDate,
            client.Name,
            client.Email
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
