using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCam.Models;
using System.Data;
using System.IO;


namespace TestCam.Controllers
{
    public class Visitor1Controller : Controller
    {
        [HttpGet]
        public ActionResult VisitorIndex1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VisitorIndex1(string base64image)
        {
            if (!String.IsNullOrEmpty(base64image))
            {
                //Maybe we should  remove unnecessary string input in front of the value
                string pictureObj = base64image.Replace("data:image/png;base64,", String.Empty);

                var model = new tbl_Picture();
                model.Picture = System.Convert.FromBase64String(pictureObj);

                //And then you can insert the model into db. 
            }
            return Json(new { result = 1 });
        }
    }   
}
