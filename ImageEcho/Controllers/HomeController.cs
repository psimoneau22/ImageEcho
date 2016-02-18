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
        public ActionResult Upload()
        {
            foreach(string fileName in Request.Files)
            {                
                // we are sending up the original file and an optimized file, the optimized file is the first one                
                HttpPostedFileBase file = Request.Files[fileName];
                byte[] fileBytes = new byte[file.ContentLength];
                file.InputStream.Read(fileBytes, 0, file.ContentLength);
                return File(fileBytes, file.ContentType, file.FileName);
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