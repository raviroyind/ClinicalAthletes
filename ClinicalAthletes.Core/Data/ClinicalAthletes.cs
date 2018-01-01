namespace ClinicalAthletes.Core.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using global::ClinicalAthletes.Entities;

    public partial class ClinicalAthletes : DbContext
    {
        public ClinicalAthletes()
            : base("name=ClinicalAthletes")
        {
        }

        
        public virtual DbSet<ExercisePlan> ExercisePlans { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<ExerciseType> ExerciseTypes { get; set; }
        public virtual DbSet<UserExercisePlanSelection> UserExercisePlanSelections { get; set; }
        public virtual DbSet<UserExerciseTypeSelection> UserExerciseTypeSelections { get; set; }
        public virtual DbSet<UserExerciseWeightSelection> UserExerciseWeightSelections { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<ExercisePlan>()
               .HasMany(e => e.ExerciseTypes);

            modelBuilder.Entity<UserExerciseTypeSelection>()
                .HasMany(e => e.UserExerciseWeightSelections);
        }
    }
}
