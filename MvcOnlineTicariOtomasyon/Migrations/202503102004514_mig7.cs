namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FaturaKalems", "Faturalar_FaturaID", "dbo.Faturas");
            DropIndex("dbo.FaturaKalems", new[] { "Faturalar_FaturaID" });
            RenameColumn(table: "dbo.FaturaKalems", name: "Faturalar_FaturaID", newName: "FaturaID");
            AlterColumn("dbo.FaturaKalems", "FaturaID", c => c.Int(nullable: false));
            CreateIndex("dbo.FaturaKalems", "FaturaID");
            AddForeignKey("dbo.FaturaKalems", "FaturaID", "dbo.Faturas", "FaturaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FaturaKalems", "FaturaID", "dbo.Faturas");
            DropIndex("dbo.FaturaKalems", new[] { "FaturaID" });
            AlterColumn("dbo.FaturaKalems", "FaturaID", c => c.Int());
            RenameColumn(table: "dbo.FaturaKalems", name: "FaturaID", newName: "Faturalar_FaturaID");
            CreateIndex("dbo.FaturaKalems", "Faturalar_FaturaID");
            AddForeignKey("dbo.FaturaKalems", "Faturalar_FaturaID", "dbo.Faturas", "FaturaID");
        }
    }
}
