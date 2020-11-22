namespace GeniusWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectUserProfileToUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeniusUserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileImage = c.String(),
                        CoverImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.GeniusUsers", "Profile_Id", c => c.Int());
            CreateIndex("dbo.GeniusUsers", "Profile_Id");
            AddForeignKey("dbo.GeniusUsers", "Profile_Id", "dbo.GeniusUserProfiles", "Id");
            DropColumn("dbo.GeniusUsers", "ProfileImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GeniusUsers", "ProfileImage", c => c.String());
            DropForeignKey("dbo.GeniusUsers", "Profile_Id", "dbo.GeniusUserProfiles");
            DropIndex("dbo.GeniusUsers", new[] { "Profile_Id" });
            DropColumn("dbo.GeniusUsers", "Profile_Id");
            DropTable("dbo.GeniusUserProfiles");
        }
    }
}
