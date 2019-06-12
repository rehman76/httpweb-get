namespace POS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posv4 : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Customer_Insert",
                p => new
                    {
                        CustomerName = p.String(),
                        CustomerAddress = p.String(),
                        Gender = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Customer]([CustomerName], [CustomerAddress], [Gender])
                      VALUES (@CustomerName, @CustomerAddress, @Gender)
                      
                      DECLARE @CustomerId int
                      SELECT @CustomerId = [CustomerId]
                      FROM [dbo].[Customer]
                      WHERE @@ROWCOUNT > 0 AND [CustomerId] = scope_identity()
                      
                      SELECT t0.[CustomerId]
                      FROM [dbo].[Customer] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[CustomerId] = @CustomerId"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Update",
                p => new
                    {
                        CustomerId = p.Int(),
                        CustomerName = p.String(),
                        CustomerAddress = p.String(),
                        Gender = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Customer]
                      SET [CustomerName] = @CustomerName, [CustomerAddress] = @CustomerAddress, [Gender] = @Gender
                      WHERE ([CustomerId] = @CustomerId)"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Delete",
                p => new
                    {
                        CustomerId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Customer]
                      WHERE ([CustomerId] = @CustomerId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Customer_Delete");
            DropStoredProcedure("dbo.Customer_Update");
            DropStoredProcedure("dbo.Customer_Insert");
        }
    }
}
