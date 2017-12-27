namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeightRequired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExerciseTypes", "WeightRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExerciseTypes", "WeightRequired");
        }
    }
}
