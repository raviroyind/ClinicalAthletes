namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserExerciseTypeSelections", "WeightRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserExerciseTypeSelections", "WeightRequired");
        }
    }
}
