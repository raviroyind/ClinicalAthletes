namespace ClinicalAthletes.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserExercisePlanSelection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserExercisePlanSelection()
        {
            UserExerciseTypeSelections = new HashSet<UserExerciseTypeSelection>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public int ExercisePlanId { get; set; }
         
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserExerciseTypeSelection> UserExerciseTypeSelections { get; set; }
    }
}
