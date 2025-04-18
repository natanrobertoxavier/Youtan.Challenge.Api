namespace Youtan.Challenge.Communication.Request;

public class RequestRegisterAuction(
    DateTime auctionDate,
    string auctionName,
    string auctionDescription,
    string auctionAddress)
{
    public DateTime AuctionDate { get; set; } = auctionDate;
    public string AuctionName { get; set; } = auctionName;
    public string AuctionDescription { get; set; } = auctionDescription;
    public string AuctionAddress { get; set; } = auctionAddress;
}
