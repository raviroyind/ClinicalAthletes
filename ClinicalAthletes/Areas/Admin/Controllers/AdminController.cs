using ClinicalAthelete.Services;
using ClinicalAthletes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicalAthletes.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin/Home
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(ExercisePlan exercisePlan)
        {
            exercisePlan.ExcelFilePath = Server.MapPath("~/ExcelFiles/" + exercisePlan.ExcelFilePath);
            DataService.Import(exercisePlan);
            return RedirectToAction("Dashboard",new {id = "success" });
        }

        public ActionResult Dashboard()
        {
            return View(DataService.GetExercisePlans());
        }
         
    }
}