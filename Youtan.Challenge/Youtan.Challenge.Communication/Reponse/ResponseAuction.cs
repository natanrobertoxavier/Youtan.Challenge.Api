namespace Youtan.Challenge.Communication.Reponse;

public class ResponseAuction(
    Guid auctionId,
    DateTime auctionDate,
    string auctionName,
    string auctionDescription,
    string auctionAddress)
{
    public Guid AuctionId { get; set; } = auctionId;
    public DateTime AuctionDate { get; set; } = auctionDate;
    public string AuctionName { get; set; } = auctionName;
    public string AuctionDescription { get; set; } = auctionDescription;
    public string AuctionAddress { get; set; } = auctionAddress;
}
