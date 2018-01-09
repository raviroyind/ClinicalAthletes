namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        CartId = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Intent = c.String(),
                        State = c.String(),
                        UserExercisePlan_Id = c.Int(),
                        UserExercisePlanSelection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExercisePlans", t => t.UserExercisePlan_Id)
                .ForeignKey("dbo.UserExercisePlanSelections", t => t.UserExercisePlanSelection_Id)
                .Index(t => t.UserExercisePlan_Id)
                .Index(t => t.UserExercisePlanSelection_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "UserExercisePlanSelection_Id", "dbo.UserExercisePlanSelections");
            DropForeignKey("dbo.Purchases", "UserExercisePlan_Id", "dbo.ExercisePlans");
            DropIndex("dbo.Purchases", new[] { "UserExercisePlanSelection_Id" });
            DropIndex("dbo.Purchases", new[] { "UserExercisePlan_Id" });
            DropTable("dbo.Purchases");
        }
    }
}
