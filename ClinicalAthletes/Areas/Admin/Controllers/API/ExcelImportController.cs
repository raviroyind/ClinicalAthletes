using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ClinicalAthletes.Entities;

namespace ClinicalAthletes.Areas.Admin.Controllers.API
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/admin/ExcelImport")]
    public class ExcelImportController : ApiController
    { 
        [HttpPost, Route("upload")]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            { 
                var postedFile = httpRequest.Files[0];
                var filePath = HttpContext.Current.Server.MapPath("~/ExcelFiles/" + postedFile.FileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                postedFile.SaveAs(filePath);
                result = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return result;
        }

        [HttpPost, Route("import")]
        public HttpResponseMessage ImportExcelData(ExercisePlan exercisePlan)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.BadRequest);
            try
            {
                ClinicalAthelete.Services.DataService.Import(exercisePlan);
                result= new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            {
                result = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return result;
        }

    }
}
