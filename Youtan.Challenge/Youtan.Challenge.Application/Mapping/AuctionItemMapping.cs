using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Entities;
using Youtan.Challenge.Application.Extensions;

namespace Youtan.Challenge.Application.Mapping;

public static class AuctionItemMapping
{
    public static AuctionItem ToEntity(this RequestRegisterAuctionItem request, Guid userId)
    {
        return new AuctionItem(
            (Domain.Enum.ItemType)request.ItemType,
            request.Description.ToUpper(),
            userId,
            request.AuctionId,
            request.StartingBid
        );
    }

    public static ResponseAuctionItem ToResponse(this AuctionItem entity)
    {
        return new ResponseAuctionItem(
            entity.Id,
            entity.ItemType.GetDescription(),
            entity.Description,
            entity.StartingBid,
            entity.Auction.ToResponse()
        );
    }
}
