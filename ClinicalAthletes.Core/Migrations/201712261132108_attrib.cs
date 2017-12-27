namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attrib : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExercisePlans", "ExcelFilePath", c => c.String(nullable: false));
            AlterColumn("dbo.ExercisePlans", "PlanName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExercisePlans", "PlanName", c => c.String());
            AlterColumn("dbo.ExercisePlans", "ExcelFilePath", c => c.String());
        }
    }
}
