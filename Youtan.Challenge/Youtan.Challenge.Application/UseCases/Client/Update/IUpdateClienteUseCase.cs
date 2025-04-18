using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Client.Update;

public interface IUpdateClienteUseCase
{
    Task<Result<MessageResult>> UpdateClientAsync(RequestUpdateClient request);
}