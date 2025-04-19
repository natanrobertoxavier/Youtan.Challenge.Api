namespace Youtan.Challenge.Communication.Request;

public class RequestRegisterBid(
    Guid auctionItemId, 
    decimal value)
{
    public Guid AuctionItemId { get; set; } = auctionItemId;
    public decimal Value { get; set; } = value;
}
