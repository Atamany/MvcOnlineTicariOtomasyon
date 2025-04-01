namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kargotest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KargoDetays",
                c => new
                    {
                        KargoDetayID = c.Int(nullable: false, identity: true),
                        UrunID = c.Int(nullable: false),
                        TakipKodu = c.String(maxLength: 10, unicode: false),
                        PersonelID = c.Int(nullable: false),
                        CariID = c.Int(nullable: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoDetayID)
                .ForeignKey("dbo.Carilers", t => t.CariID, cascadeDelete: true)
                .ForeignKey("dbo.Personels", t => t.PersonelID, cascadeDelete: true)
                .ForeignKey("dbo.Uruns", t => t.UrunID, cascadeDelete: true)
                .Index(t => t.UrunID)
                .Index(t => t.PersonelID)
                .Index(t => t.CariID);
            
            CreateTable(
                "dbo.KargoTakips",
                c => new
                    {
                        KargoTakipID = c.Int(nullable: false, identity: true),
                        TakipKodu = c.String(maxLength: 10, unicode: false),
                        Aciklama = c.String(maxLength: 100, unicode: false),
                        TarihZaman = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoTakipID);
            
            AddColumn("dbo.Uruns", "Urun_UrunID", c => c.Int());
            CreateIndex("dbo.Uruns", "Urun_UrunID");
            AddForeignKey("dbo.Uruns", "Urun_UrunID", "dbo.Uruns", "UrunID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uruns", "Urun_UrunID", "dbo.Uruns");
            DropForeignKey("dbo.KargoDetays", "UrunID", "dbo.Uruns");
            DropForeignKey("dbo.KargoDetays", "PersonelID", "dbo.Personels");
            DropForeignKey("dbo.KargoDetays", "CariID", "dbo.Carilers");
            DropIndex("dbo.KargoDetays", new[] { "CariID" });
            DropIndex("dbo.KargoDetays", new[] { "PersonelID" });
            DropIndex("dbo.KargoDetays", new[] { "UrunID" });
            DropIndex("dbo.Uruns", new[] { "Urun_UrunID" });
            DropColumn("dbo.Uruns", "Urun_UrunID");
            DropTable("dbo.KargoTakips");
            DropTable("dbo.KargoDetays");
        }
    }
}
