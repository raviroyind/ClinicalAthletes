using ClinicalAthletes.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using ClinicalAthletes.Models;
using System.Data.SqlClient;

namespace ClinicalAthelete.Services
{
    public static class DataService
    {
        private static ClinicalAthletes.Core.Data.ClinicalAthletes dbContext;
  
        public static bool Import(ExercisePlan exercisePlan)
        {
            try
            {
                exercisePlan.EntryDate = DateTime.Now;
                exercisePlan.IsActive = true;
                 
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    dbContext.ExercisePlans.Add(exercisePlan);
                    dbContext.SaveChanges();
                    exercisePlan.ExerciseTypes = ImportExcerciseTypesFromExcel(exercisePlan);
                    return true;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        internal static void UpdatePlanStatus(int id, bool isActive)
        {
            try
            {
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    var result = dbContext.ExercisePlans.SingleOrDefault(p => p.Id == id);
                    if (result != null)
                    {
                        result.IsActive = isActive;
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        internal static UserExercisePlanSelection GetUserExercisePlanSelection(int userExercisePlanSelectionId, string userId)
        { 
            try
            { 
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                { 
                    UserExercisePlanSelection userExercisePlanSelection = dbContext.UserExercisePlanSelections.Include(e=>e.UserExerciseTypeSelections).SingleOrDefault(u => u.Id.Equals(userExercisePlanSelectionId) && u.UserId.Equals(userId));

                    //List<UserExerciseTypeSelection> lst = new List<UserExerciseTypeSelection>();
                    //lst.AddRange(userExercisePlanSelection.UserExerciseTypeSelections.Where(u => u.WeightRequired == true));
                    //userExercisePlanSelection.UserExerciseTypeSelections = lst;

                    return userExercisePlanSelection;
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static SelectViewModel GetSelectView(int id)
        {
            try
            {
                List<ExerciseType> list;
                dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes();
                list = dbContext.ExerciseTypes.Where(e => e.ExercisePlanId.Equals(id)).ToList();
                return new SelectViewModel()
                {
                    ExercisePlanId = id,
                    ExerciseTypeList =list,
                    ExerciseTypeCount = list.Count,
                };
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<ExerciseViewModel> GetExercises(int exerciseTypeId)
        {
            List<ExerciseViewModel> exerciseViewModelList = new List<ExerciseViewModel>();
            List<Exercise> list;
            try
            {
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    list = dbContext.Exercises.Where(e => e.ExerciseTypeId.Equals(exerciseTypeId)).ToList();
                }
                foreach (Exercise exercise in list)
                {
                    exerciseViewModelList.Add(new ExerciseViewModel
                    {
                        Id = exercise.Id,
                        Name = exercise.Name
                    });
                }

                return exerciseViewModelList;
            }
            catch (Exception ex)
            {
                if (null != dbContext) {
                    dbContext.Dispose();
                }
                GetExercises(exerciseTypeId);
            }

            return exerciseViewModelList;
        }

        internal static string GetExerciseTypeName(int exerciseTypeId)
        {
            try
            {
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                   ExerciseType exerciseType = dbContext.ExerciseTypes.SingleOrDefault(e => e.Id.Equals(exerciseTypeId));
                   return exerciseType.Name;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static UserExerciseTypeSelection GetUserExerciseTypeSelectionById(int userExerciseTypeSelectionId)
        {
            try
            {
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    UserExerciseTypeSelection userExerciseTypeSelection = dbContext.UserExerciseTypeSelections.SingleOrDefault(e => e.Id.Equals(userExerciseTypeSelectionId));
                    return userExerciseTypeSelection;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static string GetExerciseName(int exerciseId)
        {
            try
            {
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    Exercise exercise = dbContext.Exercises.SingleOrDefault(e => e.Id.Equals(exerciseId));
                    return exercise.Name;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static int InsertUserExercisePlanSelection(UserExercisePlanSelection userExercisePlanSelection)
        {
            try
            {
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    dbContext.UserExercisePlanSelections.Add(userExercisePlanSelection);
                    dbContext.SaveChanges();
                    return userExercisePlanSelection.Id;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal static void InsertUserExerciseTypeSelections(List<UserExerciseTypeSelection> userExerciseTypeSelectionList)
        {
            try
            {
                foreach(UserExerciseTypeSelection entity in userExerciseTypeSelectionList)
                {
                    entity.ExerciseTypeName = GetExerciseTypeName(entity.ExerciseTypeId);
                    entity.ExerciseName = GetExerciseName(entity.ExerciseId);
                }
            }
            catch (Exception ex1)
            {
                throw ex1;
            }
             
            try
            {
                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    dbContext.UserExerciseTypeSelections.AddRange(userExerciseTypeSelectionList);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal static ExercisePlan GetExercisePlan(int exercisePlanId)
        {
            ExercisePlan exercisePlan;
            try
            {
                dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes();
                exercisePlan = dbContext.ExercisePlans.Find(exercisePlanId);
                return exercisePlan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<ExercisePlan> GetExercisePlans()
        {
            List<ExercisePlan> listExercisePlan;
            try
            {
                dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes();
                listExercisePlan = dbContext.ExercisePlans.Include(p => p.ExerciseTypes).ToList();
                return listExercisePlan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<ExercisePlan> GetActiveExercisePlans()
        {
            List<ExercisePlan> listExercisePlan;
            try
            {
                dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes();
                dbContext.Configuration.LazyLoadingEnabled = true;
                listExercisePlan = dbContext.ExercisePlans.Where(e => e.IsActive).ToList();
                return listExercisePlan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<ExerciseType> GetExerciseTypesForCurrentPlan(int exercisePlanId)
        {
            dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes();
                var result = dbContext.ExerciseTypes.Where(e => e.ExercisePlanId.Equals(exercisePlanId)).ToList();
                return result;
           
        }

        private static ICollection<ExerciseType> ImportExcerciseTypesFromExcel(ExercisePlan exercisePlan)
        {

            List<ExerciseType> listExerciseType = new List<ExerciseType>();
             
            using (ExcelPackage xlPackage = new ExcelPackage(new System.IO.FileInfo(exercisePlan.ExcelFilePath)))
            {
                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                var rowCnt = worksheet.Dimension.End.Row;
                var colCnt = worksheet.Dimension.End.Column;

                //1. Loop each column & get Exercise Type with its excercises
                for(int i = 1; i <= colCnt; i++)//starting from two since 1 will have WeightRequired.
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(worksheet.Cells[2, i].Value)))
                    {
                        listExerciseType.Add(new ExerciseType
                        {
                            ExercisePlanId = exercisePlan.Id,
                            Name = (string)worksheet.Cells[2, i].Value,//starting from two since 1 will have WeightRequired.
                            WeightRequired = GetWeightRequired(Convert.ToString(worksheet.Cells[1, i].Value)),
                            EntryDate = DateTime.Now,

                        });
                    }
                }

                using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                {
                    dbContext.ExerciseTypes.AddRange(listExerciseType);
                    dbContext.SaveChanges();
                }
                 
                //2. Add Exercises to ExerciseType
                for (int c=1;c<= colCnt; c++)
                {
                    List<Exercise> list = new List<Exercise>();
                    for (int r = 3; r <= rowCnt; r++)
                    {
                        string cellVal = (string)worksheet.Cells[r,c].Value;

                        if (!string.IsNullOrEmpty(cellVal))
                        {
                            list.Add(new Exercise
                            {
                                Name= cellVal,
                                ExerciseTypeId= listExerciseType[c-1].Id,
                                EntryDate=DateTime.Now,
                                IsActive=true
                            });
                        }
                    }

                    using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
                    {
                        dbContext.Exercises.AddRange(list);
                        dbContext.SaveChanges();
                    }
                }
                 
            }

            return listExerciseType;
        }

        private static bool GetWeightRequired(string value)
        {
            bool isWeightRequired = true;
            try
            {
                if (string.IsNullOrEmpty(value))
                    isWeightRequired = true;
                else if (Convert.ToString(value).Trim().ToLower() == "nw")
                    isWeightRequired = false;
            }
            catch
            {
                //do something.
            }

            return isWeightRequired;
        }


        internal static List<UserExerciseWeightSelection> GetUserExerciseWeightSelections(string userExerciseTypeSelectionIds,string weekNumber, string dayNumber)
        {
            using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
            {
               var param = new SqlParameter[]
                {
                     new SqlParameter("@UserExerciseTypeSelectionId", userExerciseTypeSelectionIds),
                     new SqlParameter("@WeekNumber", weekNumber),
                     new SqlParameter("@DayNumber", dayNumber)
                };
                    
                var lst = dbContext.UserExerciseWeightSelections.SqlQuery("GetUserExerciseWeightSelections @UserExerciseTypeSelectionId,@WeekNumber,@DayNumber", param).ToList();
                return lst;
            }
            
        }

        internal static List<UserExerciseWeightSelection> GetUserExerciseWeightSelectionsWeekly(string userExerciseTypeSelectionIds, string weekNumber)
        {
            using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
            {
                var param = new SqlParameter[]
                 {
                     new SqlParameter("@UserExerciseTypeSelectionId", userExerciseTypeSelectionIds),
                     new SqlParameter("@WeekNumber", weekNumber)
                 };

                var lst = dbContext.UserExerciseWeightSelections.SqlQuery("GetUserExerciseWeightSelectionsWeekly @UserExerciseTypeSelectionId,@WeekNumber", param).ToList();
                return lst;
            }

        }

        internal static void InsertPurchases(Purchase purchase)
        {
            using (dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes())
            {
                dbContext.Purchases.Add(purchase);
                dbContext.SaveChanges();
            }
        }
    }
}
