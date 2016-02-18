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
            Dictionary<HttpPostedFileBase, byte[]> files = new Dictionary<HttpPostedFileBase, byte[]>();
            foreach(string fileName in Request.Files)
            {                
                // we are sending up the original file and an optimized file, the optimized file is the first one                
                HttpPostedFileBase file = Request.Files[fileName];
                byte[] fileBytes = new byte[file.ContentLength];
                file.InputStream.Read(fileBytes, 0, file.ContentLength);
                file.SaveAs(Path.Combine(Server.MapPath("~/Images/"), DateTime.Now.ToString("hhmmss") + file.FileName));

                files[file] = fileBytes;
            }

            if (files.Count > 0)
            {                
                var first = files.First().Key;
                return Content(string.Format("data:{0};base64,{1}", first.ContentType, Convert.ToBase64String(files[first])));
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