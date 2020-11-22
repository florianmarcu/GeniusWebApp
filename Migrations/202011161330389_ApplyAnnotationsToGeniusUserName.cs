namespace GeniusWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationsToGeniusUserName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.GeniusUsers", name: "Profile_Id", newName: "ProfileId_Id");
            RenameIndex(table: "dbo.GeniusUsers", name: "IX_Profile_Id", newName: "IX_ProfileId_Id");
            AlterColumn("dbo.GeniusUsers", "FirstName", c => c.String(nullable: false, maxLength: 26));
            AlterColumn("dbo.GeniusUsers", "LastName", c => c.String(nullable: false, maxLength: 26));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GeniusUsers", "LastName", c => c.String());
            AlterColumn("dbo.GeniusUsers", "FirstName", c => c.String());
            RenameIndex(table: "dbo.GeniusUsers", name: "IX_ProfileId_Id", newName: "IX_Profile_Id");
            RenameColumn(table: "dbo.GeniusUsers", name: "ProfileId_Id", newName: "Profile_Id");
        }
    }
}
