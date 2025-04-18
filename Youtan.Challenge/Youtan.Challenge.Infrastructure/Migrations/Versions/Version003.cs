using FluentMigrator;

namespace Youtan.Challenge.Infrastructure.Migrations.Versions;

[Migration((long)NumberVersions.CreateAuctionsTable, "Create auction table")]
public class Version003 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var table = VersionBase.InsertStandardColumns(Create.Table("Auctions"));

        table
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("AuctionDate").AsDateTime().NotNullable()
            .WithColumn("AuctionName").AsString(100).NotNullable()
            .WithColumn("AuctionDescription").AsString(500).NotNullable()
            .WithColumn("AuctionAddress").AsString(200).NotNullable();
    }
}