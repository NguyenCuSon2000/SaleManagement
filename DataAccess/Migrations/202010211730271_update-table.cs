namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DetalOders", newName: "DetailOrders");
            AddColumn("dbo.ImportBills", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.OderForms", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.DetailOrders", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.DetailImportBills", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.DetailImportBills", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.OderForms", "State", c => c.Boolean(nullable: false));
            AlterColumn("dbo.OderForms", "DateShip", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DetailOrders", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Sales", "Percent", c => c.Double(nullable: false));
            DropColumn("dbo.ImportBills", "Total");
            DropColumn("dbo.OderForms", "Total");
            DropColumn("dbo.DetailOrders", "Sum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DetailOrders", "Sum", c => c.String());
            AddColumn("dbo.OderForms", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.ImportBills", "Total", c => c.Double(nullable: false));
            AlterColumn("dbo.Sales", "Percent", c => c.Short(nullable: false));
            AlterColumn("dbo.DetailOrders", "Quantity", c => c.Short(nullable: false));
            AlterColumn("dbo.OderForms", "DateShip", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.OderForms", "State", c => c.String());
            AlterColumn("dbo.DetailImportBills", "Price", c => c.String());
            AlterColumn("dbo.DetailImportBills", "Quantity", c => c.String());
            DropColumn("dbo.DetailOrders", "Price");
            DropColumn("dbo.OderForms", "Price");
            DropColumn("dbo.ImportBills", "Price");
            RenameTable(name: "dbo.DetailOrders", newName: "DetalOders");
        }
    }
}
