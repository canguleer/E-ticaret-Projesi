using E_ticaret_projesi.DB;
using E_ticaret_projesi.Models.Account;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_ticaret_projesi.Controllers
{
    public class AccountController : BaseController
    {
        UdemyETicaretDBEntities db = new UdemyETicaretDBEntities();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Account.RegisterModel user)
        {
            string message = string.Empty;
            var customer = db.Members.Where(w => w.Email == user.Members.Email && w.Password == user.Members.Password).FirstOrDefault();
            if (customer == null)
            {
                if (user.rePassword != user.Members.Password)
                {
                    message = ("Şifreler uyuşmuyor!");
                }

                if (db.Members.Any(x => x.Email == user.Members.Email))
                {
                    message = ("Zaten bu email adresi kayıtlıdır!");
                }
                else
                {
                    try
                    {
                        user.Members.MemberType = DB.MemberType.Customer;
                        user.Members.AddedDate = DateTime.Now;
                        db.Members.Add(user.Members);
                        db.SaveChanges();
                        return RedirectToAction("Login", "Account");

                    }
                    catch (Exception ex)
                    {
                        message = "Hata: " + ex.Message;
                    }
                }
            }
            else
            {
                message = ("Kullanıcı OLuşturma Hatası");
            }
            ViewBag.ReError = message;
            return View();
        }
        public ActionResult Login()
        {
            if (Session["LogonUser"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Account.LoginModel model)
        {
            string message = string.Empty;
            var user = db.Members.Where(w => w.Email == model.Members.Email && w.Password == model.Members.Password).FirstOrDefault();
            if (user != null)
            {
                try
                {
                    Session["LogonUser"] = user;
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    message = "Hata: " + ex.Message;
                }
            }
            else
            {
                message = ("Kullanıcı adı veya şifre hatalı!");
            }
            ViewBag.reError = message;
            return View();
        }

        public ActionResult LogOut()
        {
            Session["LogonUser"] = null;
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Profil(int id = 0)
        {
            if (id == 0)
            {
                id = base.CurrentUserId();
            }
            var user = db.Members.Where(w => w.Id == id).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ProfilModel model = new ProfilModel()
            {
                Members = user
            };
            return View(model);

        }
        public ActionResult ProfilEdit()
        {
            int id = base.CurrentUserId();
            var user = db.Members.Where(w => w.Id == id).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ProfilModel model = new ProfilModel()
            {
                Members = user
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ProfilEdit(ProfilModel model)
        {
            string message = string.Empty;
            try
            {
                int id = base.CurrentUserId();
                var updateMember = db.Members.Where(w => w.Id == id).FirstOrDefault();
                updateMember.ModifiedDate = DateTime.Now;
                updateMember.Name = model.Members.Name;
                updateMember.Surname = model.Members.Surname;
                updateMember.Bio = model.Members.Bio;

                if (string.IsNullOrEmpty(model.Members.Password) == false)
                {
                    updateMember.Password = model.Members.Password;
                }
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var folder = Server.MapPath("/Content/Images/uploads");
                    var fileName = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, fileName));

                    var filePath = "/Content/Images/uploads/" + fileName;
                    updateMember.ProfileImageName = filePath;
                }
                //db.Entry(updateMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                ViewBag.myError = message;
                return View();
            }

        }
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            var member = db.Members.Where(x => x.Email == email).FirstOrDefault();

            if (member == null)
            {
                ViewBag.MyError = "Böyle bir kullanıcı bulunamadı.";
                return View();             
            }
            else
            {
                var body = "Şifeniz: " + member.Password;
                MyMail mail = new MyMail(member.Email, "Şifremi Unuttum", body);
                mail.SendMail();
                TempData["Info"] = email +" "+ "mail adresinize şifreniz başarıyla gönderilmiştir.";
                return RedirectToAction("Login", "Account");
            }
           
        }

    }
}