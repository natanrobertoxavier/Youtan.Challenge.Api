namespace Youtan.Challenge.Domain.Repositories.Contracts.Bid;

public interface IBidReadOnly
{
    Task<Entities.Bid?> RecoverBidByAuctionItemIdAsync(Guid bidId);
}
