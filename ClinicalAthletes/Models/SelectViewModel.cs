namespace ClinicalAthletes.Models
{
    using ClinicalAthletes.Entities;
    using System.Collections.Generic;

    public class SelectViewModel
    {
        public int ExercisePlanId { get; set; }
        public int ExerciseTypeCount { get; set; }

        public List<ExerciseType> ExerciseTypeList { get; set; }

        public string ExerciseTypeIds { get; set; }
        public UserExercisePlanSelection UserExercisePlanSelection { get; set; }
    }
}