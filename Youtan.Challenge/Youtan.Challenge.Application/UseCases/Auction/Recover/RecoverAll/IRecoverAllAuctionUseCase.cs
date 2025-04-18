using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Application.UseCases.Auction.Recover.RecoverAll;

public interface IRecoverAllAuctionUseCase
{
    Task<Result<IEnumerable<ResponseAuction>>> RecoverAllAsync(int page, int pageSize);
}
