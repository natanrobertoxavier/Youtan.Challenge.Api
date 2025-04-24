using System.Text.Json.Serialization;

namespace Youtan.Challenge.Communication.Reponse;

public class ResponseAuction
{
    public ResponseAuction(
        Guid auctionId,
        DateTime auctionDate, 
        string auctionName, 
        string auctionDescription, 
        string auctionAddress)
    {
        AuctionId = auctionId;
        AuctionDate = auctionDate;
        AuctionName = auctionName;
        AuctionDescription = auctionDescription;
        AuctionAddress = auctionAddress;
    }

    public ResponseAuction(
        Guid auctionId, 
        DateTime auctionDate, 
        string auctionName, 
        string auctionDescription, 
        string auctionAddress, 
        IEnumerable<ResponseAuctionItem> auctionItems)
    {
        AuctionId = auctionId;
        AuctionDate = auctionDate;
        AuctionName = auctionName;
        AuctionDescription = auctionDescription;
        AuctionAddress = auctionAddress;
        AuctionItems = auctionItems;
    }

    public Guid AuctionId { get; set; }
    public DateTime AuctionDate { get; set; }
    public string AuctionName { get; set; }
    public string AuctionDescription { get; set; }
    public string AuctionAddress { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<ResponseAuctionItem> AuctionItems { get; set; }
}
