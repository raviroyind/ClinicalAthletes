using ClinicalAthelete.Services;
using ClinicalAthletes.Entities;
using ClinicalAthletes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
 
namespace ClinicalAthletes.Controllers.API
{
    [Authorize]
    [RoutePrefix("api/exercise")]
    public class ExerciseAPIController : ApiController
    {
        [HttpPost, Route("getPlans")]
        public List<ExercisePlan> GetExercisePlans()
        {
            return DataService.GetExercisePlans();
        }

        [HttpPost, Route("getActivePlans")]
        public List<ExercisePlan> GetActiveExercisePlans()
        {
            return DataService.GetActiveExercisePlans();
        }

        [HttpGet, Route("getExerciseTypes")]
        public List<ExerciseType> GetExerciseTypesForCurrentPlan(int exercisePlanId)
        {
            return DataService.GetExerciseTypesForCurrentPlan(exercisePlanId);
        }
        [HttpGet, Route("getExercises")]
        public List<ExerciseViewModel> GetExercises(int exerciseTypeId)
        {
            return DataService.GetExercises(exerciseTypeId);
        }

        [HttpPost, Route("saveUserExercisePlanSelection")]
        public int SaveUserExercisePlanSelections(UserExercisePlanSelection userExercisePlanSelection)
        {
            userExercisePlanSelection.UserId = User.Identity.Name;
            return DataService.InsertUserExercisePlanSelection(userExercisePlanSelection);
        }

        [HttpPost, Route("saveUserExerciseTypeSelection")]
        public void SaveUserSelection(List<UserExerciseTypeSelection> userExerciseTypeSelectionList)
        {
            DataService.InsertUserExerciseTypeSelections(userExerciseTypeSelectionList);
        }

        [HttpGet, Route("getExerciseTypeName")]
        public string GetExerciseTypeName(int exerciseTypeId)
        {
            return DataService.GetExerciseTypeName(exerciseTypeId);
        }

        [HttpGet, Route("getExerciseName")]
        public string GetExerciseName(int exerciseId)
        {
            return DataService.GetExerciseName(exerciseId);
        }


        [HttpPost, Route("saveUserExerciseWeightSelections")]
        public void SaveUserExerciseWeightSelections(List<UserExerciseWeightSelection> userExerciseWeightSelectionList)
        {
            using(ClinicalAthletes.Core.Data.ClinicalAthletes dbContext=new Core.Data.ClinicalAthletes())
            {
                dbContext.UserExerciseWeightSelections.AddRange(userExerciseWeightSelectionList);
                dbContext.SaveChanges();
            }
        }
    }
}
