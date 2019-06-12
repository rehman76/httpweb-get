namespace POS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PosDbV3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "CustomerName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "CustomerName", c => c.String());
        }
    }
}
