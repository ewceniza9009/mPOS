using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mPOS.POCO;
using mPOS.WebAPI.Utilities;
using WebGrease.Css.Extensions;

namespace mPOS.WebAPI.Repository
{
    public class TrnSales : IRead<POCO.TrnSales, POCO.TrnSalesFilter, POCO.FilterMethods>, IRepository<POCO.TrnSales>
    {
        public POCO.TrnSales Read(long id)
        {
            POCO.TrnSales result;

            var mappingProfile = new Mapping.MappingProfileForTrnSales(); ;

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.TrnSales
                    .SingleOrDefault(x => x.Id == id);

                result = mappingProfile.mapper.Map<POCO.TrnSales>(data);
            }

            return result;
        }

        public IEnumerable<POCO.TrnSales> BulkRead(TrnSalesFilter filter = null, FilterMethods filterMethods = null)
        {
            IEnumerable<POCO.TrnSales> result;

            var dynamicFilter = Utilities.Filterer<POCO.TrnSalesFilter>.GetFilter(filter, filterMethods);
            var mappingProfile = new Mapping.MappingProfileForTrnSales();

            using (var ctx = new Data.posDataContext())
            {
                IEnumerable<Data.TrnSale> data;

                if (filter != null)
                {
                    var enumerable = dynamicFilter as Filter[] ?? dynamicFilter.ToArray();
                    var filterExpression = Utilities.ExpressionBuilder
                        .GetExpression<Data.TrnSale>(enumerable.ToList()).Compile();

                    data = ctx.TrnSales.Where(filterExpression);
                }
                else
                {
                    data = ctx.TrnSales;
                }

                result = mappingProfile.mapper.Map<List<POCO.TrnSales>>(data);
            }

            return result;
        }

        public long Save(POCO.TrnSales t)
        {
            Data.TrnSale result;
            var mappingProfile = new Mapping.MappingProfileForTrnSalesReverse();

            using (var ctx = new Data.posDataContext())
            {
                if (t.Id != 0)
                {
                    foreach (var tLine in t.TrnSalesLines.Where(x => x.Id == 0))
                    {
                        tLine.DiscountId = 2;
                        tLine.DiscountRate = 0;
                        tLine.DiscountAmount = 0;
                        tLine.TaxId = 9;
                        tLine.TaxRate = 0;
                        tLine.TaxAmount = 0;
                        tLine.SalesAccountId = 159;
                        tLine.AssetAccountId = 74;
                        tLine.CostAccountId = 238;
                        tLine.TaxAccountId = 238;
                        tLine.SalesLineTimeStamp = DateTime.Now;
                    }

                    result = ctx.TrnSales.SingleOrDefault(x => x.Id == t.Id);

                    mappingProfile.mapper.Map(t, result);

                    ctx.TrnSalesLines.InsertAllOnSubmit(result.TrnSalesLines.Where(x => x.Id == 0));
                    ctx.TrnSalesLines.DeleteAllOnSubmit(result.TrnSalesLines.Where(x => x.IsDeleted));
                }
                else
                {
                    var preSalesNumber = ctx.TrnSales?.Max(x => x.SalesNumber) ?? "0001-000000";
                    var splitSalesNumber = preSalesNumber.Split('-');
                    var maxSalesNumber = Int64.Parse(splitSalesNumber[1]);
                    var newSalesNumberLng = maxSalesNumber + 1000001;

                    var newSalesNumber = $"0001-{newSalesNumberLng.ToString().Substring(1, 6)}";

                    t.PeriodId = 1;
                    t.SalesNumber = newSalesNumber;
                    t.ManualInvoiceNumber = newSalesNumber;
                    t.AccountId = 64;
                    t.TermId = 7;
                    t.SalesAgent = 1;
                    t.TerminalId = 1;
                    t.PreparedBy = 1;
                    t.CheckedBy = 1;
                    t.ApprovedBy = 1;
                    t.IsCancelled = false;
                    t.PaidAmount = 0;
                    t.CreditAmount = 0;
                    t.DebitAmount = 0;
                    t.Remarks = "POS Mobile";
                    t.EntryUserId = 1;
                    t.EntryDateTime = DateTime.Now;
                    t.UpdateUserId = 1;
                    t.UpdateDateTime = DateTime.Now;

                    foreach (var tLine in t.TrnSalesLines)
                    {
                        tLine.DiscountId = 2;
                        tLine.DiscountRate = 0;
                        tLine.DiscountAmount = 0;
                        tLine.TaxId = 9;
                        tLine.TaxRate = 0;
                        tLine.TaxAmount = 0;
                        tLine.SalesAccountId = 159;
                        tLine.AssetAccountId = 74;
                        tLine.CostAccountId = 238;
                        tLine.TaxAccountId = 238;
                        tLine.SalesLineTimeStamp = DateTime.Now;
                    }

                    result = mappingProfile.mapper.Map<Data.TrnSale>(t);

                    ctx.TrnSales?.InsertOnSubmit(result);
                }

                ctx.SubmitChanges();
            }

            return result?.Id ?? 0;
        }

        public void Delete(long id)
        {
            using (var ctx = new Data.posDataContext())
            {
                if (id > 0)
                {
                    var result = ctx.TrnSales.SingleOrDefault(x => x.Id == id);

                    ctx.TrnSales.DeleteOnSubmit(result);
                    ctx.SubmitChanges();
                }
            }
        }

        //Queries
        public List<POCO.MstCustomer> GetCustomers()
        {
            IEnumerable<POCO.MstCustomer> result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstCustomer, POCO.MstCustomer>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstCustomers;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstCustomer>, IEnumerable<POCO.MstCustomer>>(data);
            }

            return result.ToList();
        }

        public List<POCO.MstItem> GetItems()
        {
            IEnumerable<POCO.MstItem> result;

            var mappingProfile = new Mapping.MappingProfile<Data.MstItem, POCO.MstItem>();

            using (var ctx = new Data.posDataContext())
            {
                var data = ctx.MstItems;

                result = mappingProfile.mapper.Map<IEnumerable<Data.MstItem>, IEnumerable<POCO.MstItem>>(data);
            }

            return result.ToList();
        }
    }
}