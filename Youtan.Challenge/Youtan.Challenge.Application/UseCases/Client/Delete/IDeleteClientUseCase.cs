using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Application.UseCases.Client.Delete;

public interface IDeleteClientUseCase
{
    Task<Result<MessageResult>> DeleteClientAsync(Guid clientId);
}
