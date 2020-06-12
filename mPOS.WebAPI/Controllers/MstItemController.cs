using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mPOS.WebAPI.Controllers
{
    public class MstItemController : Controller
    {
        public JsonResult Get(long id)
        {
            var repos = new Repository.MstItem();
            var result = repos.Read(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BulkGet(POCO.MstItemFilter content)
        {
            var repos = new Repository.MstItem();
            var result = content.filterMethods == null ?
                repos.BulkRead() :
                repos.BulkRead(content, content.filterMethods);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(POCO.MstItem content)
        {
            var repos = new Repository.MstItem();
            var result = repos.Save(content);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var repos = new Repository.MstItem();
            repos.Delete(id);
        }

        public JsonResult GetUnits()
        {
            var repos = new Repository.MstItem();
            var result = repos.GetUnits();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}