using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.Mapping;

public static class AuctionMapping
{
    public static Domain.Entities.Auction ToEntity(this RequestRegisterAuction request, Guid userId)
    {
        return new Domain.Entities.Auction(
            userId,
            request.AuctionDate,
            request.AuctionName.ToUpper(),
            request.AuctionDescription.ToUpper(),
            request.AuctionAddress.ToUpper()
        );
    }
    public static Communication.Reponse.ResponseAuction ToResponse(this Domain.Entities.Auction entity)
    {
        return new Communication.Reponse.ResponseAuction(
            entity.Id,
            entity.AuctionDate,
            entity.AuctionName,
            entity.AuctionDescription,
            entity.AuctionAddress
        );
    }
}
