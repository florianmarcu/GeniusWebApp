namespace GeniusWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedGeniusUserProfileFieldName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.GeniusUsers", name: "ProfileId_Id", newName: "Profile_Id");
            RenameIndex(table: "dbo.GeniusUsers", name: "IX_ProfileId_Id", newName: "IX_Profile_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.GeniusUsers", name: "IX_Profile_Id", newName: "IX_ProfileId_Id");
            RenameColumn(table: "dbo.GeniusUsers", name: "Profile_Id", newName: "ProfileId_Id");
        }
    }
}
