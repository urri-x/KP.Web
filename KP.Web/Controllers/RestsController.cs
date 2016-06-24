using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using KP.WebApi.Helpers;
using KP.WebApi.Models;
using KP.WebApi.Query;

namespace KP.WebApi.Controllers
{
    public class RestsController : ApiViewController
    {
        [HttpPost]
        public IViewResult ViewCurrentPersonalRests( PersonalRestParams restParams)
        {
            return View("~/Views/PersonalRests.cshtml", GetPersonsRests(restParams) );
        }

        [HttpGet]
        public IViewResult RestParams()
        {
            return View("~/Views/PersonalRestParams.cshtml", new PersonalRestParams());
        }
        public IEnumerable<object> GetPodrs(string term="")
        {
            var stringToFind = term ?? "";
            var result =  new PodrQuery().All(DateTime.Now)
                .TopLevel()
                .Where(p=>p.Shortname.Contains(stringToFind))
                .Select(p => new {id = p.Id, value = p.Shortname});
            return result.ToList();
        }


        private IEnumerable<PersonalRest> GetPersonsRests(PersonalRestParams restParams)
        {
            var restQuery = new PersonalRestQuery();
            return restQuery.All(restParams.QueryDate)
                .OnDate(restParams.QueryDate)
                .ForPodr(restParams.IdPodr)
                .ToList();

        }
        
    }
}
