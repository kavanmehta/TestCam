using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCam.Models;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;


namespace TestCam.Controllers
{
    public class VisitorController : Controller
    {
        TestCamEntities db = new TestCamEntities();

        [HttpGet]
        public ActionResult VisitorIndex()
        {
            Session["CapturedImage"] = "";
            tbl_Personal personal = new tbl_Personal();
            var lastVisitorPassNumber = db.tbl_Personal.OrderByDescending(c => c.ID).FirstOrDefault();
            var date  = DateTime.Now.ToString("yyyy");
            if (lastVisitorPassNumber == null)
            {
                personal.VisitorPassNumber = "NCSPUN" + Convert.ToString(date) + "1" ;
            }
            else
            {
                personal.VisitorPassNumber = "NCSPUN" + (Convert.ToString(date) + lastVisitorPassNumber.ID);
            }
            return View(personal);
        }

        [HttpPost]
        public ContentResult GetCapture()
        {
            string url = Session["CapturedImage"].ToString();
            //Session["CapturedImage"] = null;
            return Content(url);
        }


        [HttpPost]
        public ActionResult Capture()
        {

            if (Request.InputStream.Length > 0)
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    string hexString = Server.UrlEncode(reader.ReadToEnd());
                    string imageName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
                    string imagePath = string.Format("~/VisitorImage/{0}.png", imageName);
                    System.IO.File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                    Session["CapturedImage"] = VirtualPathUtility.ToAbsolute(imagePath);
                }
            }

            return View();
        }

        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        [HttpPost]
        public ActionResult VisitorIndex(TestCam.Models.tbl_Personal model)
        {
            if (ModelState.IsValid)
            {
                model.ImagePath = Session["CapturedImage"].ToString();
                db.tbl_Personal.Add(model);
                db.SaveChanges();
                TempData["Success"] = "Visitor added Successfully!";
                return RedirectToAction("VisitorIndex");
            }
            //return Json(model);
            return View(model);
        }

        //Visitor's List
        [HttpGet]
        public ActionResult VisitorList()
        {
            GetListItems();
            return View();
        }


        public void GetListItems()
        {
            var result = db.tbl_Personal.ToList();
            ViewBag.VisitorList = result;
        }

        //Edit Visitor
        [HttpGet]
        public ActionResult VisitorEdit(int Id)
        {
            var std = db.tbl_Personal.Where(s => s.ID == Id).FirstOrDefault();
            return View(std);
        }
    }
}
