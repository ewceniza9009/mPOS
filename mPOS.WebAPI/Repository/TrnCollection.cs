using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using mPOS.POCO;
using mPOS.WebAPI.Data;
using mPOS.WebAPI.Mapping;
using mPOS.WebAPI.Utilities;

namespace mPOS.WebAPI.Repository
{
    public partial class TrnCollection : IRead<POCO.TrnCollection, TrnSalesFilter, FilterMethods>, IRepository<POCO.TrnCollection>
    {
        public POCO.TrnCollection Read(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<POCO.TrnCollection> BulkRead(TrnSalesFilter filter, FilterMethods filterMethods)
        {
            throw new NotImplementedException();
        }

        public long Save(POCO.TrnCollection t)
        {
            Data.TrnCollection result = new Data.TrnCollection();
            var mappingProfile = new MappingProfileForTrnCollectionReverse();

            using (var ctx = new posDataContext())
            {
                if (t.Id != 0)
                {
                    //TODO: Start collection view on Xamarin Project
                }
                else
                {
                    var preORNumber = ctx.TrnCollections?.Where(x => x.CollectionNumber != "NA")?.Max(x => x.CollectionNumber) ?? "001-0001-000000";
                    var splitORNumber = preORNumber.Split('-');
                    var maxORNumber = long.Parse(splitORNumber[2]);
                    var newORNumberLng = maxORNumber + 1000001;

                    var newORNumber = $"001-0001-{newORNumberLng.ToString().Substring(1, 6)}";

                    t.PeriodId = 1;
                    t.CollectionNumber = newORNumber;
                    t.ManualORNumber = newORNumber;
                    t.TerminalId = 1;
                    t.PreparedBy = 1;
                    t.CheckedBy = 1;
                    t.ApprovedBy = 1;
                    t.IsCancelled = false;
                    t.IsLocked = true;
                    t.EntryUserId = 1;
                    t.EntryDateTime = DateTime.Now;
                    t.UpdateUserId = 1;
                    t.UpdateDateTime = DateTime.Now;

                    result = mappingProfile.mapper.Map<Data.TrnCollection>(t);

                    ctx.TrnCollections?.InsertOnSubmit(result);
                }

                ctx.SubmitChanges();
            }

            return result?.Id ?? 0;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}