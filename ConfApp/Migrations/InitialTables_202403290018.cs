﻿using FluentMigrator;

namespace ConfApp.Migrations
{
    [Migration(202403290018)]
    public class InitialTables_202403290018 : Migration
    {
        public override void Down()
        {
            Delete.Table("applications");
        }
        public override void Up()
        {
            Create.Table("applications")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().Unique()
                .WithColumn("author").AsGuid().NotNullable()
                .WithColumn("activity").AsString().Nullable()
                .WithColumn("name").AsString(100).Nullable()
                .WithColumn("description").AsString(300).Nullable()
                .WithColumn("outline").AsString(1000).Nullable()
                .WithColumn("datetime").AsDateTime2().NotNullable()
                .WithColumn("sended").AsBoolean().NotNullable();
        }
    }
}
