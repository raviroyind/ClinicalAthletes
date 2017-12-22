using ClinicalAthelete.Services;
using ClinicalAthletes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

    }
}
