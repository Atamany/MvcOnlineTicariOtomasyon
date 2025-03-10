namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faturas", "Toplam", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Faturas", "Saat", c => c.String(maxLength: 5, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Faturas", "Saat", c => c.DateTime(nullable: false));
            DropColumn("dbo.Faturas", "Toplam");
        }
    }
}
