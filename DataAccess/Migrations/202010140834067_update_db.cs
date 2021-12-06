namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_db : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubMenus", "Menu_Id", "dbo.Menus");
            DropIndex("dbo.SubMenus", new[] { "Menu_Id" });
            DropTable("dbo.Menus");
            DropTable("dbo.SubMenus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Menu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SubMenus", "Menu_Id");
            AddForeignKey("dbo.SubMenus", "Menu_Id", "dbo.Menus", "Id");
        }
    }
}
