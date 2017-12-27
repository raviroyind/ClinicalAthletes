namespace ClinicalAthletes.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserExerciseTypeSelection
    {
        public int Id { get; set; }

        public int UserExercisePlanSelectionId { get; set; }

        public int ExerciseTypeId { get; set; }

        public string ExerciseTypeName { get; set; }

        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }

        public virtual UserExercisePlanSelection UserExercisePlanSelection { get; set; }
    }
}
