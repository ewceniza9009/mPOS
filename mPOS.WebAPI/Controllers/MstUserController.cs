using System.Web.Mvc;
using mPOS.POCO;

namespace mPOS.WebAPI.Controllers
{
    public class MstUserController : Controller
    {
        [HttpPost]
        public JsonResult CanLogin(MstUser user)
        {
            var userRepos = new Repository.MstUser();
            var result = userRepos.IsLoginSuccess(user.UserName, user.Password);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}