using System.Web.Mvc;
using mPOS.POCO;

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
        public JsonResult BulkGet(MstItemFilter content)
        {
            var repos = new Repository.MstItem();
            var result = content.FilterMethods == null
                ? repos.BulkRead()
                : repos.BulkRead(content, content.FilterMethods);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(MstItem content)
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

        public JsonResult GetItemCategories()
        {
            var repos = new Repository.MstItem();
            var result = repos.GetItemCategories();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaxes()
        {
            var repos = new Repository.MstItem();
            var result = repos.GetTaxes();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}