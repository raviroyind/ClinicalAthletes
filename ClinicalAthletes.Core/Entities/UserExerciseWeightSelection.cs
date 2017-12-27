namespace ClinicalAthletes.Entities
{
    public class UserExerciseWeightSelection
    {
        public int Id { get; set; }
        public int UserExerciseTypeSelectionId { get; set; }
        public int WeekNumber { get; set; }
        public int DayNumber { get; set; }
        public int SetNumber { get; set; }
        public int Weight { get; set; }
        public virtual UserExerciseTypeSelection UserExerciseTypeSelection { get; set; }
    }
}
