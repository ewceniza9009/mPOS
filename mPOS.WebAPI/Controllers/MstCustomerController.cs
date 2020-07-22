using System.Web.Mvc;
using mPOS.POCO;

namespace mPOS.WebAPI.Controllers
{
    public class MstCustomerController : Controller
    {
        public JsonResult Get(long id)
        {
            var repos = new Repository.MstCustomer();
            var result = repos.Read(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BulkGet(MstCustomerFilter content)
        {
            var repos = new Repository.MstCustomer();
            var result = content.filterMethods == null
                ? repos.BulkRead()
                : repos.BulkRead(content, content.filterMethods);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(MstCustomer content)
        {
            var repos = new Repository.MstCustomer();
            var result = repos.Save(content);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var repos = new Repository.MstCustomer();
            repos.Delete(id);
        }
    }
}