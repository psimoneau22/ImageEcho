using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;

namespace ImageEcho.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Upload(string returnType, string ful, string med, string low)
        {
            
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string filePath = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(file.FileName));
                file.SaveAs(filePath);

                if (returnType == "base64")
                {
                    return Content(Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath)));
                }

                if (returnType == "url")
                {
                    return Content("~/Images/" + Path.GetFileName(file.FileName));
                }

                return File(filePath, file.ContentType);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}