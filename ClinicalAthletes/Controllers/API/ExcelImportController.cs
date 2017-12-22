using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ClinicalAthletes.Entities;

namespace ClinicalAthelete.Controllers.API
{
    [Authorize]
    [RoutePrefix("api/ExcelImport")]
    public class ExcelImportController : ApiController
    {
        public string Get()
        {
            return "Hello world";
        }

        [HttpPost, Route("upload")]
        public HttpResponseMessage Post()
        {

            HttpResponseMessage result = null;
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
            try
            {
                Services.DataService.Import(exercisePlan);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (System.Exception ex)
            { 
                throw ex;
            }
        }

    }
}
