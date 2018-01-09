namespace ClinicalAthletes.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "UserExercisePlan_Id", "dbo.ExercisePlans");
            DropForeignKey("dbo.Purchases", "UserExercisePlanSelection_Id", "dbo.UserExercisePlanSelections");
            DropIndex("dbo.Purchases", new[] { "UserExercisePlan_Id" });
            DropIndex("dbo.Purchases", new[] { "UserExercisePlanSelection_Id" });
            AddColumn("dbo.Purchases", "ExercisePlanId", c => c.Int(nullable: false));
            AddColumn("dbo.Purchases", "UserExercisePlanSelectionId", c => c.Int(nullable: false));
            DropColumn("dbo.Purchases", "UserExercisePlan_Id");
            DropColumn("dbo.Purchases", "UserExercisePlanSelection_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "UserExercisePlanSelection_Id", c => c.Int());
            AddColumn("dbo.Purchases", "UserExercisePlan_Id", c => c.Int());
            DropColumn("dbo.Purchases", "UserExercisePlanSelectionId");
            DropColumn("dbo.Purchases", "ExercisePlanId");
            CreateIndex("dbo.Purchases", "UserExercisePlanSelection_Id");
            CreateIndex("dbo.Purchases", "UserExercisePlan_Id");
            AddForeignKey("dbo.Purchases", "UserExercisePlanSelection_Id", "dbo.UserExercisePlanSelections", "Id");
            AddForeignKey("dbo.Purchases", "UserExercisePlan_Id", "dbo.ExercisePlans", "Id");
        }
    }
}
