using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.Mapping;

public static class UserMapping
{
    public static Domain.Entities.User ToEntity(this RequestRegisterUser request, string password)
    {
        return new Domain.Entities.User(
            request.Name,
            request.Email.ToLower(),
            password
        );
    }

    public static ResponseLogin ToResponseLogin(this Domain.Entities.User user, string token)
    {
        return new ResponseLogin(
            user.Name,
            user.Email,
            token
        );
    }
}
