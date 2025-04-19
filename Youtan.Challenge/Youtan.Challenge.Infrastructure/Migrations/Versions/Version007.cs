using FluentMigrator;

namespace Youtan.Challenge.Infrastructure.Migrations.Versions;

[Migration((long)NumberVersions.CreateBidsTable, "Create bids table")]
public class Version007 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var table = VersionBase.InsertStandardColumns(Create.Table("Bids"));

        table
            .WithColumn("Value").AsDecimal(18, 2).NotNullable()
            .WithColumn("ClientId").AsGuid().NotNullable()
                .ForeignKey("Clients", "Id")
            .WithColumn("AuctionItemId").AsGuid().NotNullable()
                .ForeignKey("AuctionItems", "Id");
    }
}