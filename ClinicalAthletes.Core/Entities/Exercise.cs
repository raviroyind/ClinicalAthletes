using System;

namespace ClinicalAthletes.Entities
{ 
    public class Exercise
    {
        public int Id { get; set; }
        public int ExerciseTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime EntryDate { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }
    }
}
