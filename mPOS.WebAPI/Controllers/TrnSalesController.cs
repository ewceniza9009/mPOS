using System;
using System.Web.Mvc;
using mPOS.POCO;

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
        public JsonResult BulkGet(TrnSalesFilter content)
        {
            var repos = new Repository.TrnSales();
            var result = content.filterMethods == null
                ? repos.BulkRead()
                : repos.BulkRead(content, content.filterMethods);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(TrnSales content)
        {
            var repos = new Repository.TrnSales();
            var result = repos.Save(content);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var repos = new Repository.TrnSales();
            repos.Delete(id);
        }

        public JsonResult GetSalesReport(string param)
        {
            var date = Convert.ToDateTime(param);

            var report = new Repository.Reports.SalesReport(new Repository.TrnSales());
            var result = report.GetSalesReport(date);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomers()
        {
            var repos = new Repository.TrnSales();
            var result = repos.GetCustomers();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItems()
        {
            var repos = new Repository.TrnSales();
            var result = repos.GetItems();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaxes()
        {
            var repos = new Repository.TrnSales();
            var result = repos.GetTaxes();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiscounts()
        {
            var repos = new Repository.TrnSales();
            var result = repos.GetDiscounts();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTerms()
        {
            var result = Repository.Common.GetTerms();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}