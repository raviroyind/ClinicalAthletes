using System.Collections.Generic;

namespace ClinicalAthletes.Entities
{
    public partial class UserExerciseTypeSelection
    {
        public int Id { get; set; }
        public int UserExercisePlanSelectionId { get; set; }
        public int ExerciseTypeId { get; set; }
        public string ExerciseTypeName { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public bool WeightRequired { get; set; }
        public virtual UserExercisePlanSelection UserExercisePlanSelection { get; set; }
        public virtual ICollection<UserExerciseWeightSelection> UserExerciseWeightSelections { get; set; }
    }
}
