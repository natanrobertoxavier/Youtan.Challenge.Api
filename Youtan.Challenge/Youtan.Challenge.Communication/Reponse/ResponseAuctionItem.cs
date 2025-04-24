using System.Text.Json.Serialization;
using Youtan.Challenge.Communication.Enums;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Communication.Reponse;

public class ResponseAuctionItem
{
    public ResponseAuctionItem(
        Guid auctionItemId, 
        string itemType, 
        string description, 
        decimal startingBid, 
        decimal increase, 
        ResponseAuction auction)
    {
        AuctionItemId = auctionItemId;
        ItemType = itemType;
        Description = description;
        StartingBid = startingBid;
        Increase = increase;
        Auction = auction;
    }
    public ResponseAuctionItem(
        Guid auctionItemId, 
        string itemType, 
        string description, 
        decimal startingBid, 
        decimal increase)
    {
        AuctionItemId = auctionItemId;
        ItemType = itemType;
        Description = description;
        StartingBid = startingBid;
        Increase = increase;
    }

    public Guid AuctionItemId { get; set; }
    public string ItemType { get; set; }
    public string Description { get; set; }
    public decimal StartingBid { get; set; }
    public decimal Increase { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResponseAuction Auction { get; set; }
}