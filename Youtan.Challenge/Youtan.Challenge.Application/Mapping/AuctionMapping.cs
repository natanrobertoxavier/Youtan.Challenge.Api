using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.Mapping;

public static class AuctionMapping
{
    public static Domain.Entities.Auction ToEntity(this RequestRegisterAuction request, Guid userId)
    {
        return new Domain.Entities.Auction(
            userId,
            request.AuctionDate,
            request.AuctionName,
            request.AuctionDescription,
            request.AuctionAddress
        );
    }
}
