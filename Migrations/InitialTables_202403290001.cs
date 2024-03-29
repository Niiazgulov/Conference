using FluentMigrator;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace ConfApp.Migrations
{
    [Migration(202403290001)]
    public class InitialTables_202403290001 : Migration
    {
        public override void Down()
        {
            Delete.Table("Applications");
        }
        public override void Up()
        {
            Create.Table("Applications")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Author").AsGuid().NotNullable()
                .WithColumn("Activity").AsString().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Description").AsString(300)
                .WithColumn("Outline").AsString(1000).NotNullable();
        }
    }
}
