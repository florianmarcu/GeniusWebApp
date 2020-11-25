namespace GeniusWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupGeniusUsers",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        GeniusUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.GeniusUser_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.GeniusUsers", t => t.GeniusUser_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.GeniusUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupGeniusUsers", "GeniusUser_Id", "dbo.GeniusUsers");
            DropForeignKey("dbo.GroupGeniusUsers", "Group_Id", "dbo.Groups");
            DropIndex("dbo.GroupGeniusUsers", new[] { "GeniusUser_Id" });
            DropIndex("dbo.GroupGeniusUsers", new[] { "Group_Id" });
            DropTable("dbo.GroupGeniusUsers");
            DropTable("dbo.Groups");
        }
    }
}
