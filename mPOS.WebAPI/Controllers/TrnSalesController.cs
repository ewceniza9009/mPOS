using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mPOS.WebAPI.Controllers
{
    public class TrnSalesController : Controller
    {
        // TODO: Routes
        public JsonResult Get(long id)
        {
            var repos = new Repository.TrnSales();
            var result = repos.Read(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BulkGet(POCO.TrnSalesFilter content)
        {
            var repos = new Repository.TrnSales();
            var result = content.filterMethods == null ?
                repos.BulkRead() :
                repos.BulkRead(content, content.filterMethods);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //TODO: Complete TrnSales Saving
        [HttpPost]
        public JsonResult Save(POCO.MstItem content)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var repos = new Repository.TrnSales();
            repos.Delete(id);
        }
    }
}