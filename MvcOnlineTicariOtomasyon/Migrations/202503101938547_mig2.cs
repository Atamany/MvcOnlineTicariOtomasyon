namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faturas", "FaturaSiraNo", c => c.String(maxLength: 6, unicode: false));
            DropColumn("dbo.Faturas", "FaturaSıraNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faturas", "FaturaSıraNo", c => c.String(maxLength: 6, unicode: false));
            DropColumn("dbo.Faturas", "FaturaSiraNo");
        }
    }
}
