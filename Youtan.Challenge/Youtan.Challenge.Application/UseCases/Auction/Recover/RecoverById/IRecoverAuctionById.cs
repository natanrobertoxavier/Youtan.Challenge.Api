using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Application.UseCases.Auction.Recover.RecoverById;

public interface IRecoverAuctionById
{
    Task<Result<ResponseAuction>> RecoverAuctionByIdAsync(Guid auctionId);
}
