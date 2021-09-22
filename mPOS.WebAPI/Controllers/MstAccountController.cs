using System.Web.Mvc;
using mPOS.POCO;
using MstAccount = mPOS.WebAPI.Repository.MstAccount;

namespace mPOS.WebAPI.Controllers
{
    public class MstAccountController : Controller
    {
        public JsonResult GetAccount()
        {
            return null;
        }

        [HttpPost]
        public JsonResult GetAccounts(MstAccountFilter filter)
        {
            var account = new MstAccount();
            var result = filter.FilterMethods == null
                ? account.BulkRead()
                : account.BulkRead(filter, filter.FilterMethods);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}