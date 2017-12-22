using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalAthletes.Entities
{
    public class ExercisePlan
    {
        public int Id { get; set; }
        public string ExcelFilePath { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime EntryDate { get; set; }
        public virtual ICollection<ExerciseType> ExerciseTypes { get; set; }
    }
}
