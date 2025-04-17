using FluentMigrator;

namespace Youtan.Challenge.Infrastructure.Migrations.Versions;

[Migration((long)NumberVersions.CreateClientsTable, "Create client table")]
public class Version002 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var table = VersionBase.InsertStandardColumns(Create.Table("Clients"));

        table
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable();
    }
}
