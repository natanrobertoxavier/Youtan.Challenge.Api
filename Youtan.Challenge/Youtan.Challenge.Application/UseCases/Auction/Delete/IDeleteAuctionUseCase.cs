using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Application.UseCases.Auction.Delete;

public interface IDeleteAuctionUseCase
{
    Task<Result<MessageResult>> DeleteAuctionAsync(Guid auctionId);
}
