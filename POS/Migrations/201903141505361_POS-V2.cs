namespace POS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POSV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "Gender");
        }
    }
}
