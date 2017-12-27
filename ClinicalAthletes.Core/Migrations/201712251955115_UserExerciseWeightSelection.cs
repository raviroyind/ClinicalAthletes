namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserExerciseWeightSelection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserExerciseWeightSelections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserExerciseTypeSelectionId = c.Int(nullable: false),
                        WeekNumber = c.Int(nullable: false),
                        DayNumber = c.Int(nullable: false),
                        SetNumber = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserExerciseTypeSelections", t => t.UserExerciseTypeSelectionId, cascadeDelete: true)
                .Index(t => t.UserExerciseTypeSelectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserExerciseWeightSelections", "UserExerciseTypeSelectionId", "dbo.UserExerciseTypeSelections");
            DropIndex("dbo.UserExerciseWeightSelections", new[] { "UserExerciseTypeSelectionId" });
            DropTable("dbo.UserExerciseWeightSelections");
        }
    }
}
