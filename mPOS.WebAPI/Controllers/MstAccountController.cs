using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mPOS.WebAPI.Controllers
{
    public class MstAccountController : Controller
    {
        public JsonResult GetAccount()
        {
            return null;
        }

        [HttpPost]
        public JsonResult GetAccounts(POCO.MstAccountFilter filter)
        {
            var account = new Repository.MstAccount();
            var result = filter.filterMethods == null ? 
                account.BulkRead() : 
                account.BulkRead(filter, filter.filterMethods);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}