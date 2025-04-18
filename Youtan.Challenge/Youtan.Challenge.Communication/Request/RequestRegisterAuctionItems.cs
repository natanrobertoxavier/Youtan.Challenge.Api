using System.Text.Json.Serialization;
using Youtan.Challenge.Communication.Enums;
using Youtan.Challenge.Communication.Filters;

namespace Youtan.Challenge.Communication.Request;

public class RequestRegisterAuctionItems(
    ItemType itemType,
    string description,
    Guid auctionId)
{
    [JsonConverter(typeof(ItemsTypeConverter))]
    public ItemType ItemType { get; set; } = itemType;
    public string Description { get; set; } = description;
    public Guid AuctionId { get; set; } = auctionId;
}