using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Entities;

namespace Youtan.Challenge.Application.Mapping;

public static class AuctionItemMapping
{
    public static AuctionItem ToEntity(this RequestRegisterAuctionItems request, Guid userId)
    {
        return new AuctionItem(
            (Domain.Enum.ItemType)request.ItemType,
            request.Description,
            userId,
            request.AuctionId
        );
    }
}
