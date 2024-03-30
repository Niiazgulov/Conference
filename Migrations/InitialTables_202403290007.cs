using FluentMigrator;

namespace ConfApp.Migrations
{
    [Migration(202403290007)]
    public class InitialTables_202403290007 : Migration
    {
        public override void Down()
        {
            Create.Table("applications")
    .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
    .WithColumn("author").AsGuid().NotNullable()
    .WithColumn("activity").AsString().NotNullable()
    .WithColumn("name").AsString(100).NotNullable()
    .WithColumn("description").AsString(300).Nullable()
    .WithColumn("outline").AsString(1000).NotNullable()
    .WithColumn("datetime").AsDateTime().NotNullable();
        }

        public override void Up()
        {
            Delete.Table("applications");
        }

    }
}
