using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.User.Login;

public interface IUserLoginUseCase
{
    Task<Result<ResponseLogin>> LoginAsync(RequestLogin request);
}
