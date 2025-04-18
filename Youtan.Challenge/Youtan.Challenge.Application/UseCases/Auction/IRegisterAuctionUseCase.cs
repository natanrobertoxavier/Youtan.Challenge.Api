using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Auction;

public interface IRegisterAuctionUseCase
{
    Task<Result<MessageResult>> RegisterAuctionAsync(RequestRegisterAuction request);
}
