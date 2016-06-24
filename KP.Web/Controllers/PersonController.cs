using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using KP.WebApi.Helpers;
using KP.WebApi.Models;
using KP.WebApi.Query;

namespace KP.WebApi.Controllers
{
    public class PersonController : ApiViewController
    {
        [HttpGet]
        [WebMethod(EnableSession = true)]
        public IViewResult ViewPerson(int id)
        {
            var personQuery = new PersonalQuery();
            var person = personQuery.One(id, DateTime.Now);
            object session = Request.Properties["MS_HttpContext"];
            
            return View("~/Views/Person.cshtml", person);
        }

        [HttpPost]
        public IViewResult EditPerson(Person person)
        {
            //var currentPerson = (Person) HttpContext.Current.Session[person.Id];
            return null;
        }
        
    }
}