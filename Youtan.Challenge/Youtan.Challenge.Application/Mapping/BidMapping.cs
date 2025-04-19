using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.Mapping;

public static class BidMapping
{
    public static Domain.Entities.Bid ToEntity(this RequestRegisterBid request, Guid clientId, decimal lastBid)
    {
        var totalBidValue = CalculateTotalBidValue(request.Value, lastBid);

        return new Domain.Entities.Bid(
            totalBidValue,
            clientId,
            request.AuctionItemId);
    }

    private static decimal CalculateTotalBidValue(decimal bidValue, decimal lastBid)
    {
        if (bidValue <= 0)
            throw new ArgumentException("O valor do lance deve ser maior que zero.", nameof(bidValue));

        return bidValue + lastBid;
    }
}
