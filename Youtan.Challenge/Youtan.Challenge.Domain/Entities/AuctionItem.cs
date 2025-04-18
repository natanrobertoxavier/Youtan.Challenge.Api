using System.Numerics;
using Youtan.Challenge.Domain.Enum;

namespace Youtan.Challenge.Domain.Entities;

public class AuctionItem : BaseEntity
{
    public AuctionItem(
        Guid auctionItemid,
        DateTime registrationDate,
        ItemType itemType,
        string description, 
        Guid userId, 
        Guid auctionId,
        decimal startingBid,
        decimal increase) : base(auctionItemid, registrationDate)
    {
        ItemType = itemType;
        Description = description;
        UserId = userId;
        AuctionId = auctionId;
        StartingBid = startingBid;
        Increase = increase;
    }

    public AuctionItem(
        ItemType itemType,
        string description, 
        Guid userId, 
        Guid auctionId,
        decimal startBid,
        decimal increase)
    {
        ItemType = itemType;
        Description = description;
        UserId = userId;
        AuctionId = auctionId;
        StartingBid = startBid;
        Increase = increase;
    }

    public AuctionItem()
    {
    }

    public ItemType ItemType { get; set; }
    public string Description { get; set; }
    public decimal StartingBid { get; set; }
    public decimal Increase { get; set; }
    public Guid UserId { get; set; }
    public Guid AuctionId { get; set; }
    public virtual Auction Auction { get; set; }
}
