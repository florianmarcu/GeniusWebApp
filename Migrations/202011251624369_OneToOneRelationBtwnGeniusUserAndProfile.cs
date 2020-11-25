namespace GeniusWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToOneRelationBtwnGeniusUserAndProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupGeniusUsers", "GeniusUser_Id", "dbo.GeniusUsers");
            DropForeignKey("dbo.GeniusUsers", "Profile_Id", "dbo.GeniusUserProfiles");
            DropIndex("dbo.GeniusUsers", new[] { "Profile_Id" });
            DropPrimaryKey("dbo.GeniusUsers");
            DropPrimaryKey("dbo.GeniusUserProfiles");
            DropColumn("dbo.GeniusUsers", "Id");
            DropColumn("dbo.GeniusUserProfiles", "Id");
            RenameColumn(table: "dbo.GroupGeniusUsers", name: "GeniusUser_Id", newName: "GeniusUser_GeniusUserId");
            RenameColumn(table: "dbo.GeniusUsers", name: "Profile_Id", newName: "GeniusUserId");
            RenameIndex(table: "dbo.GroupGeniusUsers", name: "IX_GeniusUser_Id", newName: "IX_GeniusUser_GeniusUserId");
            AddColumn("dbo.GeniusUserProfiles", "GeniusUserProfileId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.GeniusUserProfiles", "GeniusUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.GeniusUsers", "GeniusUserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.GeniusUsers", "GeniusUserId");
            AddPrimaryKey("dbo.GeniusUserProfiles", "GeniusUserProfileId");
            CreateIndex("dbo.GeniusUsers", "GeniusUserId");
            AddForeignKey("dbo.GroupGeniusUsers", "GeniusUser_GeniusUserId", "dbo.GeniusUsers", "GeniusUserId", cascadeDelete: true);
            AddForeignKey("dbo.GeniusUsers", "GeniusUserId", "dbo.GeniusUserProfiles", "GeniusUserProfileId");
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.GeniusUsers", "GeniusUserId", "dbo.GeniusUserProfiles");
            DropForeignKey("dbo.GroupGeniusUsers", "GeniusUser_GeniusUserId", "dbo.GeniusUsers");
            DropIndex("dbo.GeniusUsers", new[] { "GeniusUserId" });
            DropPrimaryKey("dbo.GeniusUserProfiles");
            DropPrimaryKey("dbo.GeniusUsers");
            DropColumn("dbo.GeniusUserProfiles", "GeniusUserId");
            DropColumn("dbo.GeniusUserProfiles", "GeniusUserProfileId");
            AddColumn("dbo.GeniusUserProfiles", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.GeniusUsers", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.GeniusUsers", "GeniusUserId", c => c.Int());
            AddPrimaryKey("dbo.GeniusUserProfiles", "Id");
            AddPrimaryKey("dbo.GeniusUsers", "Id");
            RenameIndex(table: "dbo.GroupGeniusUsers", name: "IX_GeniusUser_GeniusUserId", newName: "IX_GeniusUser_Id");
            RenameColumn(table: "dbo.GeniusUsers", name: "GeniusUserId", newName: "Profile_Id");
            RenameColumn(table: "dbo.GroupGeniusUsers", name: "GeniusUser_GeniusUserId", newName: "GeniusUser_Id");
            CreateIndex("dbo.GeniusUsers", "Profile_Id");
            AddForeignKey("dbo.GeniusUsers", "Profile_Id", "dbo.GeniusUserProfiles", "Id");
            AddForeignKey("dbo.GroupGeniusUsers", "GeniusUser_Id", "dbo.GeniusUsers", "Id", cascadeDelete: true);
        }
    }
}
