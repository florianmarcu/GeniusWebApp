namespace GeniusWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGeniusUserCollection : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GeniusUsers (FirstName, LastName) VALUES ('Florian', 'Marcu') ");
        }
        
        public override void Down()
        {
        }
    }
}
