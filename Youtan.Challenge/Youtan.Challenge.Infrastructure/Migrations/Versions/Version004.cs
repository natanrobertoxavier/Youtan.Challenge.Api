using FluentMigrator;

namespace Youtan.Challenge.Infrastructure.Migrations.Versions;

[Migration((long)NumberVersions.CreateAuctionItemsTable, "Create auction items table")]
public class Version004 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var table = VersionBase.InsertStandardColumns(Create.Table("AuctionItems"));

        table
            .WithColumn("ItemType").AsInt32().NotNullable()
                .WithColumnDescription("Tipo do item, valores permitidos: 0 (Vehicle), 1 (Property)")
            .WithColumn("Description").AsString(500).NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("AuctionId").AsGuid().NotNullable()
                .ForeignKey("Auctions", "Id");

        Execute.Sql("ALTER TABLE AuctionItems ADD CONSTRAINT CK_AuctionItems_ItemType CHECK (ItemType IN (0, 1));");
    }
}
