using Domain;
using FluentMigrator;

namespace ConfApp.Migrations
{
    [Migration(202403290002)]
    public class InitialSeed_202403290002 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Applications")
                .Row(new Applications
                {
                    Id = new Guid("c000eb37-a5aa-4753-a011-d8c384fcb3b4"),
                    Author = new Guid("28161a9e-2ca0-4160-971b-cfb94d3c8a51"),
                    Activity = "Masterclass",
                    Name = "Application # 1",
                    Description = "This is a short Description",
                    Outline = "This is a long long, very long, no I mean - really veryyyyy loooonggg Outlineeeeeee"
                });
        }
        public override void Up()
        {
            Insert.IntoTable("Applications")
                .Row(new Applications
                {
                    Id = new Guid("c000eb37-a5aa-4753-a011-d8c384fcb3b4"),
                    Author = new Guid("28161a9e-2ca0-4160-971b-cfb94d3c8a51"),
                    Activity = "Masterclass",
                    Name = "Application # 1",
                    Description = "This is a short Description",
                    Outline = "This is a long long, very long, no I mean - really veryyyyy loooonggg Outlineeeeeee"
                });
        }
    }
}
