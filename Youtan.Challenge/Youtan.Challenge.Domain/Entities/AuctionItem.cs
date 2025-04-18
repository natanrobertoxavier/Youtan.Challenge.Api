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
        Guid auctionId) : base(auctionItemid, registrationDate)
    {
        ItemType = itemType;
        Description = description;
        UserId = userId;
        AuctionId = auctionId;
    }

    public AuctionItem(
        ItemType itemType,
        string description, 
        Guid userId, 
        Guid auctionId)
    {
        ItemType = itemType;
        Description = description;
        UserId = userId;
        AuctionId = auctionId;
    }

    public AuctionItem()
    {
    }

    public ItemType ItemType { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public Guid AuctionId { get; set; }
    public virtual Auction Auction { get; set; }
}
