using ClinicalAthelete.Services;
using ClinicalAthletes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicalAthletes.Models
{
    public class WeeklyViewModel
    {
        public WeeklyViewModel(int userExercisePlanSelectionId, string UserId)
        {
            this.ExercisePlanId = ExercisePlanId;
            UserExercisePlanSelection = DataService.GetUserExercisePlanSelection(userExercisePlanSelectionId, UserId);
        }
        public int ExercisePlanId { get; set; }

        public UserExercisePlanSelection UserExercisePlanSelection { get; set; }
        public ICollection<UserExerciseTypeSelection> UserExerciseTypeSelectionList
        {
            get
            {
                return this.UserExercisePlanSelection.UserExerciseTypeSelections;
            }
            set
            {
                UserExerciseTypeSelectionList = value;
            }
        }
         
    }
}