using System.Text.Json.Serialization;
using Youtan.Challenge.Communication.Enums;
using Youtan.Challenge.Communication.Filters;

namespace Youtan.Challenge.Communication.Request;

public class RequestRegisterAuctionItem(
    ItemType itemType,
    string description,
    decimal startingBid,
    Guid auctionId)
{
    [JsonConverter(typeof(ItemsTypeConverter))]
    public ItemType ItemType { get; set; } = itemType;
    public string Description { get; set; } = description;
    public decimal StartingBid { get; set; } = startingBid;
    public Guid AuctionId { get; set; } = auctionId;
}