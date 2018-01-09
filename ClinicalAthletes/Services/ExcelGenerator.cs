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
            FileInfo newFile = new FileInfo(HttpContext.Current.Server.MapPath(@"~/UserExcels/" + PlanId + "_" + UserId + ".xlsx"));

            // If there is any file having same name as 'Sample2.xlsx', then delete it first
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(HttpContext.Current.Server.MapPath(@"~/UserExcels/" + PlanId +"_"+ UserId + ".xlsx"));
            }


          //  DataService.GetUserExerciseWeightSelections("331,332,333", "2", "2");

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

                var ListExTypes= userExercisePlanSelection.UserExerciseTypeSelections.OrderByDescending(x => x.WeightRequired).ToList();

                foreach (UserExerciseTypeSelection userExerciseTypeSelection in ListExTypes)
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
                      
                }

                #region Cell Addresses...

                int Week1DayOneStart = 6;
                int Week1DayTwoStart = 14;
                int Week1DayThreeStart = 22;

                int Week2DayOneStart = 6;
                int Week2DayTwoStart = 14;
                int Week2DayThreeStart = 22;

                int Week3DayOneStart = 6;
                int Week3DayTwoStart = 14;
                int Week3DayThreeStart = 22;

                int Week4DayOneStart = 6;
                int Week4DayTwoStart = 14;
                int Week4DayThreeStart = 22;

                int Week5DayOneStart = 6;
                int Week5DayTwoStart = 14;
                int Week5DayThreeStart = 22;

                int Week6DayOneStart = 30;
                int Week6DayTwoStart = 38;
                int Week6DayThreeStart = 46;

                int Week7DayOneStart = 30;
                int Week7DayTwoStart = 38;
                int Week7DayThreeStart = 46;

                int Week8DayOneStart = 30;
                int Week8DayTwoStart = 38;
                int Week8DayThreeStart = 46;

                int Week9DayOneStart = 30;
                int Week9DayTwoStart = 38;
                int Week9DayThreeStart = 46;

                int Week10DayOneStart = 30;
                int Week10DayTwoStart = 38;
                int Week10DayThreeStart = 46;

                int Week11DayOneStart = 30;
                int Week11DayTwoStart = 38;
                int Week11DayThreeStart = 46;

                int Week12DayOneStart = 30;
                int Week12DayTwoStart = 38;
                int Week12DayThreeStart = 46;

                #endregion Cell Addresses...

                #region Weekly Table...
                foreach (UserExerciseTypeSelection userExerciseTypeSelection in userExercisePlanSelection.UserExerciseTypeSelections)
                {

                    #region Week One...
                    List<UserExerciseWeightSelection> lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "1");

                    
                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(1) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["E" + Week1DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week1DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(1) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["E" + Week1DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week1DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(1) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["E" + Week1DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week1DayThreeStart++;
                        } 
                    }
                    #endregion Week One...

                    #region Week Two...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "2");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(2) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["H" + Week2DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week2DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(2) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["H" + Week2DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week2DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(2) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["H" + Week2DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week2DayThreeStart++;
                        }
                    }
                    #endregion Week Two...

                    #region Week Three...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "3");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(3) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["K" + Week3DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week3DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(3) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["K" + Week3DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week3DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(3) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["K" + Week3DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week3DayThreeStart++;
                        }
                    }
                    #endregion Week Three...
                     
                    #region Week Four...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "4");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(4) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["N" + Week4DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week4DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(4) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["N" + Week4DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week4DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(4) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["N" + Week4DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week4DayThreeStart++;
                        }
                    }
                    #endregion Week Four...

                    #region Week Five...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "5");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(5) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["Q" + Week5DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week5DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(5) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["Q" + Week5DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week5DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(5) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["Q" + Week5DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week5DayThreeStart++;
                        }
                    }
                    #endregion Week Five...

                    #region Week Six...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "6");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(6) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["E" + Week6DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week6DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(6) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["E" + Week6DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week6DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(6) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["E" + Week6DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week6DayThreeStart++;
                        }
                    }
                    #endregion Week Six...

                    #region Week Seven...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "7");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(7) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["H" + Week7DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week7DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(7) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["H" + Week7DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week7DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(7) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["H" + Week7DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week7DayThreeStart++;
                        }
                    }
                    #endregion Week Seven...

                    #region Week Eight...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "8");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(8) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["K" + Week8DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week8DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(8) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["K" + Week8DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week8DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(8) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["K" + Week8DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week8DayThreeStart++;
                        }
                    }
                    #endregion Week Eight...

                    #region Week Nine...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "9");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(9) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["N" + Week9DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week9DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(9) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["N" + Week9DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week9DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(9) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["N" + Week9DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week9DayThreeStart++;
                        }
                    }
                    #endregion Week Nine...

                    #region Week Ten...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "10");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(10) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["Q" + Week10DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week10DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(10) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["Q" + Week10DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week10DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(10) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["Q" + Week10DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week10DayThreeStart++;
                        }
                    }
                    #endregion Week Ten...

                    #region Week Eleven...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "11");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(11) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["T" + Week11DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week11DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(11) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["T" + Week11DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week11DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(11) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["T" + Week11DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week11DayThreeStart++;
                        }
                    }
                    #endregion Week Eleven...

                    #region Week Twelve...

                    lst = DataService.GetUserExerciseWeightSelectionsWeekly(userExerciseTypeSelection.Id.ToString(), "12");

                    foreach (UserExerciseWeightSelection userExerciseWeightSelection in lst)
                    {
                        userExerciseWeightSelection.UserExerciseTypeSelection = DataService.GetUserExerciseTypeSelectionById(userExerciseWeightSelection.UserExerciseTypeSelectionId);

                        if (userExerciseWeightSelection.WeekNumber.Equals(12) && userExerciseWeightSelection.DayNumber.Equals(1) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["W" + Week12DayOneStart].Value = userExerciseWeightSelection.Weight;
                            Week12DayOneStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(12) && userExerciseWeightSelection.DayNumber.Equals(2) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["W" + Week12DayTwoStart].Value = userExerciseWeightSelection.Weight;
                            Week12DayTwoStart++;
                        }

                        if (userExerciseWeightSelection.WeekNumber.Equals(12) && userExerciseWeightSelection.DayNumber.Equals(3) && userExerciseWeightSelection.UserExerciseTypeSelection.WeightRequired)
                        {
                            worksheet.Cells["W" + Week12DayThreeStart].Value = userExerciseWeightSelection.Weight;
                            Week12DayThreeStart++;
                        }
                    }
                    #endregion Week Twelve...
                }

                #endregion Weekly Table...

                package.Save();

            }

            return newFile.Name;
        }
    }
}