using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalAthletes.Entities
{ 
    public class ExerciseType
    { 
        public int Id { get; set; }
        public int ExercisePlanId { get; set; }
        public string Name { get; set; }
        public bool WeightRequired { get; set; }
        public DateTime EntryDate { get; set; }
        public virtual ExercisePlan ExercisePlan { get; set; }
        public virtual List<Exercise> Exercises { get; set; }

    }
}
