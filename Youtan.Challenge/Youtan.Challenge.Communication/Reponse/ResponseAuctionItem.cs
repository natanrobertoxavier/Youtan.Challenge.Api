using Youtan.Challenge.Communication.Enums;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Communication.Reponse;

public class ResponseAuctionItem(
    Guid auctionItemId,
    string itemType,
    string description,
    decimal startingBid,
    decimal increase,
    ResponseAuction auction)
{
    public Guid AuctionItemId { get; set; } = auctionItemId;
    public string ItemType { get; set; } = itemType;
    public string Description { get; set; } = description;
    public decimal StartingBid { get; set; } = startingBid;
    public decimal Increase { get; set; } = increase;
    public ResponseAuction Auction { get; set; } = auction;
}