using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Auction.Register;

public interface IRegisterAuctionUseCase
{
    Task<Result<MessageResult>> RegisterAuctionAsync(RequestRegisterAuction request);
}
