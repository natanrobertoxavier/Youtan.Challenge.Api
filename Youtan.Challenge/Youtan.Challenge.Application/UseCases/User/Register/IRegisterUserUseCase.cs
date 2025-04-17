using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<Result<MessageResult>> RegisterUserAsync(RequestRegisterUser request);
}
