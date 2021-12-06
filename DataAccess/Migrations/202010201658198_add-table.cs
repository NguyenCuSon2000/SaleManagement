namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetailImportBills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.String(),
                        Price = c.String(),
                        ImportBill_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportBills", t => t.ImportBill_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.ImportBill_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ImportBills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Double(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.OderForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Phone = c.String(),
                        Total = c.Double(nullable: false),
                        Note = c.String(),
                        State = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateShip = c.DateTimeOffset(nullable: false, precision: 7),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.DetalOders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.String(),
                        Quantity = c.Short(nullable: false),
                        OderForm_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OderForms", t => t.OderForm_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.OderForm_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Img = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Percent = c.Short(nullable: false),
                        DateBegin = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        HotLine = c.String(),
                        Facebook = c.String(),
                        Zalo = c.String(),
                        Instagram = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "Sale_Id", c => c.Int());
            CreateIndex("dbo.Products", "Sale_Id");
            AddForeignKey("dbo.Products", "Sale_Id", "dbo.Sales", "Id");
            DropColumn("dbo.Products", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Image", c => c.String());
            DropForeignKey("dbo.OderForms", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "Sale_Id", "dbo.Sales");
            DropForeignKey("dbo.Images", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.DetalOders", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.DetailImportBills", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.DetalOders", "OderForm_Id", "dbo.OderForms");
            DropForeignKey("dbo.ImportBills", "User_Id", "dbo.Users");
            DropForeignKey("dbo.DetailImportBills", "ImportBill_Id", "dbo.ImportBills");
            DropIndex("dbo.Images", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "Sale_Id" });
            DropIndex("dbo.DetalOders", new[] { "Product_Id" });
            DropIndex("dbo.DetalOders", new[] { "OderForm_Id" });
            DropIndex("dbo.OderForms", new[] { "User_Id" });
            DropIndex("dbo.ImportBills", new[] { "User_Id" });
            DropIndex("dbo.DetailImportBills", new[] { "Product_Id" });
            DropIndex("dbo.DetailImportBills", new[] { "ImportBill_Id" });
            DropColumn("dbo.Products", "Sale_Id");
            DropTable("dbo.Settings");
            DropTable("dbo.Sales");
            DropTable("dbo.Images");
            DropTable("dbo.DetalOders");
            DropTable("dbo.OderForms");
            DropTable("dbo.ImportBills");
            DropTable("dbo.DetailImportBills");
        }
    }
}
