namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig8 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Yapilacaks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Yapilacaks",
                c => new
                    {
                        YapilacakID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 100, unicode: false),
                        Durum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.YapilacakID);
            
        }
    }
}
