using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicalAthletes.Entities
{
    public class UserExercisePlanSelection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExercisePlanId { get; set; }
        public virtual ExercisePlan ExercisePlan { get; set; }
        public virtual List<UserExerciseTypeSelection> UserExerciseTypeSelections { get; set; }
    }

    public class UserExerciseTypeSelection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserExercisePlanSelectionId { get; set; }
        public int ExerciseTypeId { get; set; }
        public string ExerciseTypeName { get; set; }
        public virtual UserExercisePlanSelection UserExercisePlanSelection { get; set; }

    }




}
