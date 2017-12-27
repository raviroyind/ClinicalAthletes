using ClinicalAthelete.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClinicalAthletes.Areas.Admin.Controllers.API
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/admin/dashboard")]
    public class DashboardController : ApiController
    {
        [HttpPost, Route("updateActive")]
        public HttpResponseMessage Post(int id,bool isActive)
        {
            try
            {
                DataService.UpdatePlanStatus(id, isActive);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

           
        }
    }
}
