namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "GenerateExcel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "GenerateExcel");
        }
    }
}
