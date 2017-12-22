using ClinicalAthletes.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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

        internal static List<ExercisePlan> GetExercisePlans()
        {
            try
            {

                dbContext = new ClinicalAthletes.Core.Data.ClinicalAthletes();
                
 
                    dbContext.Configuration.LazyLoadingEnabled = true;
                    var result = dbContext.ExercisePlans.ToList();
                    return result;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                for(int i = 1; i <= colCnt; i++)
                {
                    listExerciseType.Add(new ExerciseType
                    {
                        ExercisePlanId= exercisePlan.Id,
                        Name= (string)worksheet.Cells[1, i].Value,
                        EntryDate=DateTime.Now,
                    });
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
                    for (int r = 2; r <= rowCnt; r++)
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
    }
}
