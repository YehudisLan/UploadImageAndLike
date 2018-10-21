using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using HWUploadImages.Data;

namespace HWUploadImagesOBM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var manager = new DBManager(Properties.Settings.Default.ConStr);
            return View( manager.GetImages());
        }
        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(string title, HttpPostedFileBase imageFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            imageFile.SaveAs(Server.MapPath("~/UploadedImages/") + fileName);
            Image im = new Image
            {
                FileName = fileName,
                Title = title,
                Likes = 0
            };
            new DBManager(Properties.Settings.Default.ConStr).AddImage(im);
            return RedirectToAction("Index");
        }
        public ActionResult ViewSingle(int id)
        {
            var manager = new DBManager(Properties.Settings.Default.ConStr);
            return View(manager.GetImage(id));
        }
        [HttpPost]
        public void AddLike(Image i)
        {
              if(Session["liked"] == null)
            {
               Session["liked"] = new List<int>();
            }
              var ids = (List<int>)Session["liked"];
            if(!ids.Contains(i.Id))
            {
            var manager = new DBManager(Properties.Settings.Default.ConStr);
            manager.AddLike(i.Id);
            ids.Add(i.Id);
                Session["liked"] = ids;
            }
        }
        public ActionResult GetLikes(Image i)
        {
            var manager = new DBManager(Properties.Settings.Default.ConStr);
           return Json(manager.GetLikes(i.Id),JsonRequestBehavior.AllowGet);
        }
    }
}