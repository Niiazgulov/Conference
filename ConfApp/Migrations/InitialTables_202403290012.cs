﻿using FluentMigrator;

namespace ConfApp.Migrations
{
    [Migration(202403290012)]
    public class InitialTables_202403290012 : Migration
    {
        public override void Down()
        {
            Delete.Table("applications");
        }
        public override void Up()
        {
            Create.Table("applications")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("author").AsGuid().NotNullable()
                .WithColumn("activity").AsString().NotNullable()
                .WithColumn("name").AsString(100).NotNullable()
                .WithColumn("description").AsString(300).Nullable()
                .WithColumn("outline").AsString(1000).NotNullable()
                .WithColumn("datetime").AsDateTime2().NotNullable();
        }
    }
}

