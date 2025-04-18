using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Auction.Update;

public interface IUpdateAuctionUseCase
{
    Task<Result<MessageResult>> UpdateAuctionAsync(RequestUpdateAuction request);
}
