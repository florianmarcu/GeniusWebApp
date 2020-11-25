namespace GeniusWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeniusUserProfiles",
                c => new
                    {
                        GeniusUserProfileId = c.Int(nullable: false, identity: true),
                        ProfileImage = c.String(),
                        CoverImage = c.String(),
                        GeniusUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GeniusUserProfileId);
            
            CreateTable(
                "dbo.GeniusUsers",
                c => new
                    {
                        GeniusUserId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 26),
                        LastName = c.String(nullable: false, maxLength: 26),
                    })
                .PrimaryKey(t => t.GeniusUserId)
                .ForeignKey("dbo.GeniusUserProfiles", t => t.GeniusUserId)
                .Index(t => t.GeniusUserId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.GroupGeniusUsers",
                c => new
                    {
                        Group_GroupId = c.Int(nullable: false),
                        GeniusUser_GeniusUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_GroupId, t.GeniusUser_GeniusUserId })
                .ForeignKey("dbo.Groups", t => t.Group_GroupId, cascadeDelete: true)
                .ForeignKey("dbo.GeniusUsers", t => t.GeniusUser_GeniusUserId, cascadeDelete: true)
                .Index(t => t.Group_GroupId)
                .Index(t => t.GeniusUser_GeniusUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupGeniusUsers", "GeniusUser_GeniusUserId", "dbo.GeniusUsers");
            DropForeignKey("dbo.GroupGeniusUsers", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.GeniusUsers", "GeniusUserId", "dbo.GeniusUserProfiles");
            DropIndex("dbo.GroupGeniusUsers", new[] { "GeniusUser_GeniusUserId" });
            DropIndex("dbo.GroupGeniusUsers", new[] { "Group_GroupId" });
            DropIndex("dbo.GeniusUsers", new[] { "GeniusUserId" });
            DropTable("dbo.GroupGeniusUsers");
            DropTable("dbo.Groups");
            DropTable("dbo.GeniusUsers");
            DropTable("dbo.GeniusUserProfiles");
        }
    }
}
