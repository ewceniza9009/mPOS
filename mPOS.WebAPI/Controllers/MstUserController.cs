using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mPOS.WebAPI.Controllers
{
    public class MstUserController : Controller
    {
        [HttpPost]
        public JsonResult CanLogin(POCO.MstUser user)
        {
            var userRepos = new Repository.MstUser();
            var result = userRepos.IsLoginSuccess(user.UserName, user.Password);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}