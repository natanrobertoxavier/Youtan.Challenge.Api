namespace Youtan.Challenge.Communication.Request;

public class RequestRegisterAuction
{
    public RequestRegisterAuction(
        DateTime auctionDate, 
        string auctionName, 
        string auctionDescription, 
        string auctionAddress)
    {
        AuctionDate = auctionDate;
        AuctionName = auctionName;
        AuctionDescription = auctionDescription;
        AuctionAddress = auctionAddress;
    }

    public RequestRegisterAuction()
    {
    }

    public DateTime AuctionDate { get; set; }
    public string AuctionName { get; set; }
    public string AuctionDescription { get; set; }
    public string AuctionAddress { get; set; }
}
