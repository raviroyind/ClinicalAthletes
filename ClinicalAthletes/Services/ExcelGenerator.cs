using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ClinicalAthletes.Entities;
using ClinicalAthelete.Services;

namespace ClinicalAthletes.Services
{
    public static class ExcelGenerator
    { 
        public static string GenerateExcel(string UserId,int PlanId)
        { 
            // Taking existing file: Here 'ClinicalAthlete.xlsx' is treated as template file
            FileInfo templateFile = new FileInfo(HttpContext.Current.Server.MapPath(@"~/OutputTemplate/ClinicalAthlete Template.xlsx"));
             
            // Making a new file for user
            FileInfo newFile = new FileInfo(HttpContext.Current.Server.MapPath(@"~/UserExcels/" +HttpContext.Current.User.Identity.Name+".xlsx"));

            // If there is any file having same name as 'Sample2.xlsx', then delete it first
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(HttpContext.Current.Server.MapPath(@"~/UserExcels/" + HttpContext.Current.User.Identity.Name + ".xlsx"));
            } 

            using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
            { 
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                if (worksheet == null)
                {
                    Exception exp = new Exception("Could not load worksheet.");
                    return exp.Message;
                }

                UserExercisePlanSelection userExercisePlanSelection = DataService.GetUserExercisePlanSelection(PlanId, HttpContext.Current.User.Identity.Name);
                if (null == userExercisePlanSelection)
                {
                    Exception exp = new Exception("Could not load exercise plan selection.");
                    return exp.Message;
                }

                int dayOneStart = 6;
                int dayTwoStart = 14;
                int dayThreeStart = 22;
                int dayFourStart = 30;
                int dayFiveStart = 38;
                int daySixStart = 46;

                foreach (UserExerciseTypeSelection userExerciseTypeSelection in userExercisePlanSelection.UserExerciseTypeSelections)
                {
                    worksheet.Cells["A" + dayOneStart.ToString()].Value = userExerciseTypeSelection.ExerciseTypeName;
                    worksheet.Cells["B" + dayOneStart.ToString()].Value = userExerciseTypeSelection.ExerciseName;
                    dayOneStart++;

                    worksheet.Cells["A" + dayTwoStart.ToString()].Value = userExerciseTypeSelection.ExerciseTypeName;
                    worksheet.Cells["B" + dayTwoStart.ToString()].Value = userExerciseTypeSelection.ExerciseName;
                    dayTwoStart++;

                    worksheet.Cells["A" + dayThreeStart.ToString()].Value = userExerciseTypeSelection.ExerciseTypeName;
                    worksheet.Cells["B" + dayThreeStart.ToString()].Value = userExerciseTypeSelection.ExerciseName;
                    dayThreeStart++;

                    worksheet.Cells["A" + dayFourStart.ToString()].Value = userExerciseTypeSelection.ExerciseTypeName;
                    worksheet.Cells["B" + dayFourStart.ToString()].Value = userExerciseTypeSelection.ExerciseName;
                    dayFourStart++;

                    worksheet.Cells["A" + dayFiveStart.ToString()].Value = userExerciseTypeSelection.ExerciseTypeName;
                    worksheet.Cells["B" + dayFiveStart.ToString()].Value = userExerciseTypeSelection.ExerciseName;
                    dayFiveStart++;

                    worksheet.Cells["A" + daySixStart.ToString()].Value = userExerciseTypeSelection.ExerciseTypeName;
                    worksheet.Cells["B" + daySixStart.ToString()].Value = userExerciseTypeSelection.ExerciseName;
                    daySixStart++;


                    using(ClinicalAthletes.Core.Data.ClinicalAthletes dbContect=new Core.Data.ClinicalAthletes())
                    {
                        List<UserExerciseWeightSelection> lst = dbContect.UserExerciseWeightSelections.Where(e => e.UserExerciseTypeSelectionId.Equals(userExerciseTypeSelection.Id)).ToList();
                        foreach (UserExerciseWeightSelection weekOne in lst.Where(w => w.WeekNumber.Equals(1)).ToList())
                        {
                             if(weekOne.SetNumber==1 && weekOne.Weight>0)
                                worksheet.Cells["E" + daySixStart.ToString()].Value = weekOne.Weight.ToString();

                            if (weekOne.SetNumber == 2 && weekOne.Weight > 0)
                                worksheet.Cells["E" + Convert.ToString(daySixStart + 1)].Value = weekOne.Weight.ToString();

                            if (weekOne.SetNumber == 3 && weekOne.Weight > 0)
                                worksheet.Cells["E" + Convert.ToString(daySixStart + 2)].Value = weekOne.Weight.ToString();

                        }
                    }


                } 

                package.Save();

            }

            return string.Empty;
        }
    }
}