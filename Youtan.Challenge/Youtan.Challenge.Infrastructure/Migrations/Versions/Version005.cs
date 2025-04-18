using FluentMigrator;

namespace Youtan.Challenge.Infrastructure.Migrations.Versions;

[Migration((long)NumberVersions.AddStartingBidToAuctionItem, "Add StartingBid column to AuctionItem table")]
public class Version005 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Alter.Table("AuctionItems")
            .AddColumn("StartingBid").AsDecimal(18, 2).NotNullable().WithDefaultValue(0);

        Execute.Sql("UPDATE AuctionItems SET StartingBid = 0 WHERE StartingBid IS NULL");
    }
}
