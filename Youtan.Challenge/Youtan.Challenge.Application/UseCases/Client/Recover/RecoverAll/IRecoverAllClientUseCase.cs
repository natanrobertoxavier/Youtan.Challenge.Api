using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Application.UseCases.Client.Recover.RecoverAll;

public interface IRecoverAllClientUseCase
{
    Task<Result<IEnumerable<ResponseClient>>> RecoverAllAsync(int page, int pageSize);
}
