namespace OrderedListApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderedListDetails",
                c => new
                    {
                        ListId = c.Int(nullable: false, identity: true),
                        ClientReferenceId = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ListId);
            
            CreateTable(
                "dbo.OrderedListItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderedListDetails", t => t.ListId, cascadeDelete: true)
                .Index(t => t.ListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderedListItem", "ListId", "dbo.OrderedListDetails");
            DropIndex("dbo.OrderedListItem", new[] { "ListId" });
            DropTable("dbo.OrderedListItem");
            DropTable("dbo.OrderedListDetails");
        }
    }
}
