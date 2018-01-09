namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "Currency", c => c.String());
            AddColumn("dbo.Purchases", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "Amount");
            DropColumn("dbo.Purchases", "Currency");
        }
    }
}
