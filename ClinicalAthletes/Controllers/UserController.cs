using ClinicalAthelete.Services;
using ClinicalAthletes.Entities;
using ClinicalAthletes.Models;
using ClinicalAthletes.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicalAthletes.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Dashboard()
        {
            return View(DataService.GetActiveExercisePlans());
        }

        public ActionResult Select(int id)
        {
            SelectViewModel selectViewModel = DataService.GetSelectView(id);
            return View(selectViewModel);
        }

        [HttpPost]
        public ActionResult Select(SelectViewModel selectViewModel)
        {
            List<ExerciseViewModel> list = DataService.GetExercises(25);

            string sJSONResponse = JsonConvert.SerializeObject(list);

            selectViewModel.UserExercisePlanSelection = new UserExercisePlanSelection
            {
                ExercisePlanId = selectViewModel.ExercisePlanId,
                UserId = HttpContext.User.Identity.Name
            };
             
            int userExercisePlanSelectionId =  DataService.InsertUserExercisePlanSelection(selectViewModel.UserExercisePlanSelection);

            return WeeklyPlan(userExercisePlanSelectionId);
        }

        public ActionResult WeeklyPlan(int userExercisePlanSelectionId)
        {
            WeeklyViewModel weeklyViewModel = new WeeklyViewModel(userExercisePlanSelectionId, User.Identity.Name);
             
            return View(weeklyViewModel);
        }

        public ActionResult Payment(int userExercisePlanSelectionId)
        {
            ExcelGenerator.GenerateExcel(User.Identity.Name, userExercisePlanSelectionId);

            return View();
        }
    }
}
