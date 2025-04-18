namespace Youtan.Challenge.Domain.Entities;

public class Auction : BaseEntity
{
    public Auction(
        Guid AuctionId,
        DateTime registrationDate,
        Guid userId,
        DateTime auctionDate,
        string auctionName,
        string auctionDescription, 
        string auctionAddress)
    {
        Id = AuctionId;
        RegistrationDate = registrationDate;
        UserId = userId;
        AuctionDate = auctionDate;
        AuctionName = auctionName;
        AuctionDescription = auctionDescription;
        AuctionAddress = auctionAddress;
    }

    public Auction(
        Guid userId,
        DateTime auctionDate,
        string auctionName,
        string auctionDescription,
        string auctionAddress)
    {
        UserId = userId;
        AuctionDate = auctionDate;
        AuctionName = auctionName;
        AuctionDescription = auctionDescription;
        AuctionAddress = auctionAddress;
    }

    public Auction()
    {
    }
    public Guid UserId { get; set; }
    public DateTime AuctionDate { get; set; }
    public string AuctionName { get; set; }
    public string AuctionDescription { get; set; }
    public string AuctionAddress { get; set; }
    public virtual ICollection<AuctionItem> AuctionItems { get; set; }
}
