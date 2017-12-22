using ClinicalAthelete.Services;
using ClinicalAthletes.Entities;
using ClinicalAthletes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicalAthletes.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(ExercisePlan exercisePlan)
        {
            exercisePlan.ExcelFilePath = Server.MapPath("~/ExcelFiles/" + exercisePlan.ExcelFilePath);
            DataService.Import(exercisePlan);
            return View();
        }
         
        public ActionResult Plans()
        {
            return View(DataService.GetExercisePlans());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}