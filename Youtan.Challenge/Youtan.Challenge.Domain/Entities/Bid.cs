namespace Youtan.Challenge.Domain.Entities;

public class Bid : BaseEntity
{
    public Bid(
        Guid bidId,
        DateTime registrationDate,
        decimal value,
        Guid clientId,
        Guid auctionItemId) : base(bidId, registrationDate)
    {
        Value = value;
        ClientId = clientId;
        AuctionItemId = auctionItemId;
    }

    public Bid(
        decimal value,
        Guid clientId,
        Guid auctionItemId)
    {
        Value = value;
        ClientId = clientId;
        AuctionItemId = auctionItemId;
    }

    public Bid()
    {
    }

    public decimal Value { get; set; }
    public Guid ClientId { get; set; }
    public Guid AuctionItemId { get; set; }
    public virtual AuctionItem AuctionItem { get; set; }
    public virtual Client Client { get; set; }
}
