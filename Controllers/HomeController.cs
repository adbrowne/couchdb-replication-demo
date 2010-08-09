using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Divan;

namespace CouchConflictDemo.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            string host = "192.168.1.6";
            int port = 5918;

            var server = new CouchServer(host, port);

            var db = server.GetDatabase("contacts");

            Console.WriteLine("Created database 'contacts'");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
