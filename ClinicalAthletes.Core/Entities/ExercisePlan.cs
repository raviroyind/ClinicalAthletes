using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalAthletes.Entities
{
    public class ExercisePlan
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Excel file path is required")]
        public string ExcelFilePath { get; set; }
        [Required(ErrorMessage = "Plan name is required")]
        public string PlanName { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime EntryDate { get; set; }
        public virtual ICollection<ExerciseType> ExerciseTypes { get; set; }
    }
}
