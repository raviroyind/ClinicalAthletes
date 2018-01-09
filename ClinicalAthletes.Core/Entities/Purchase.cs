using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalAthletes.Entities
{
    public class Purchase
    {
      [Key]
      public string Id { get; set; }
      public string UserId { get; set; }
      public int ExercisePlanId { get; set; }
      public int UserExercisePlanSelectionId { get; set; }
      public string CartId { get; set; }
      public DateTime CreateTime { get; set; }
      public string Intent { get; set; }
      public string GenerateExcel { get; set; }
      public string Currency { get; set; }
      public double Amount { get; set; }
      public string State { get; set; }
      
      
    }
}
