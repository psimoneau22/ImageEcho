using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ImageEcho.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Upload()
        {
            HttpPostedFileBase file = Request.Files[0];
            string filePath = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(file.FileName));
            file.SaveAs(filePath);
            return File(filePath, file.ContentType);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}