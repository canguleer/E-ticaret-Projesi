using E_ticaret_projesi.DB;
using E_ticaret_projesi.Models;
using E_ticaret_projesi.Models.i;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_ticaret_projesi.Controllers
{
    public class HomeController : BaseController
    {
        UdemyETicaretDBEntities db = new UdemyETicaretDBEntities();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Product(int id = 0)
        {
            var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ProductModel model = new ProductModel()
            {
                Product = product,
                Comments = product.Comments.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Product(DB.Comments comments)
        {
            string message = string.Empty;
            try
            {
                comments.Member_Id = base.CurrentUserId();
                comments.AddedDate = DateTime.Now;
                db.Comments.Add(comments);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            ViewBag.myError = message;
            return RedirectToAction("Product", "Home");
        }


    }
}
