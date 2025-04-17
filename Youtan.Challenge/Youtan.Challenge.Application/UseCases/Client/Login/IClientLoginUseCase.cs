using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Client.Login;

public interface IClientLoginUseCase
{
    Task<Result<ResponseLogin>> LoginAsync(RequestLogin request);
}
