using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_ticaret_projesi.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected DB.Members CurrentUser()
        {
            if (Session["LogonUser"] == null)
            {
                return null;
            }
            return (DB.Members)Session["LogonUser"];
        }

        protected int CurrentUserId()
        {
            if (Session["LogonUser"] == null)
            {
                return 0;
            }
            return ((DB.Members)Session["LogonUser"]).Id;
        }
    }
}