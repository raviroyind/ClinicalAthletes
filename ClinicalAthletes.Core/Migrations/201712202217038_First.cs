namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExercisePlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExcelFilePath = c.String(),
                        PlanName = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExerciseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExercisePlanId = c.Int(nullable: false),
                        Name = c.String(),
                        EntryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExercisePlans", t => t.ExercisePlanId, cascadeDelete: true)
                .Index(t => t.ExercisePlanId);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExerciseTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExerciseTypes", t => t.ExerciseTypeId, cascadeDelete: true)
                .Index(t => t.ExerciseTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseTypes", "ExercisePlanId", "dbo.ExercisePlans");
            DropForeignKey("dbo.Exercises", "ExerciseTypeId", "dbo.ExerciseTypes");
            DropIndex("dbo.Exercises", new[] { "ExerciseTypeId" });
            DropIndex("dbo.ExerciseTypes", new[] { "ExercisePlanId" });
            DropTable("dbo.Exercises");
            DropTable("dbo.ExerciseTypes");
            DropTable("dbo.ExercisePlans");
        }
    }
}
