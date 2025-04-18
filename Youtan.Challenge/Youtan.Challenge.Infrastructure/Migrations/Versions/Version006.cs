using FluentMigrator;

namespace Youtan.Challenge.Infrastructure.Migrations.Versions;

[Migration((long)NumberVersions.AddIncreasedToAuctionItem, "Add Increase column to AuctionItem table")]
public class Version006 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        Alter.Table("AuctionItems")
            .AddColumn("Increase").AsDecimal(10, 2).NotNullable().WithDefaultValue(0);

        Execute.Sql("UPDATE AuctionItems SET Increase = 0 WHERE Increase IS NULL");
    }
}
