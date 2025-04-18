using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Recover.RecoverAll;

public interface IRecoverAllAuctionItemUseCase
{
    Task<Result<IEnumerable<ResponseAuctionItem>>> RecoverAllAsync(int page, int pageSize);
}
