namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database_delete_table_menu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Menu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.Menu_Id)
                .Index(t => t.Menu_Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        View = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Color = c.String(),
                        Size = c.String(),
                        State = c.Boolean(nullable: false),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProductCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_Id)
                .Index(t => t.ProductCategory_Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PassWord = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Image = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        UserName = c.String(),
                        RoleUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleUsers", t => t.RoleUser_Id)
                .Index(t => t.RoleUser_Id);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Heading = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleUser_Id", "dbo.RoleUsers");
            DropForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.SubMenus", "Menu_Id", "dbo.Menus");
            DropIndex("dbo.Users", new[] { "RoleUser_Id" });
            DropIndex("dbo.Products", new[] { "ProductCategory_Id" });
            DropIndex("dbo.SubMenus", new[] { "Menu_Id" });
            DropTable("dbo.Slides");
            DropTable("dbo.Users");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.News");
            DropTable("dbo.SubMenus");
            DropTable("dbo.Menus");
        }
    }
}
