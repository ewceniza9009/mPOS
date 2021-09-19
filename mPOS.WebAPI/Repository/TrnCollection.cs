using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using mPOS.POCO;
using mPOS.POCO.Report;
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

        public OfficalReceipt GetOfficialReceipt(int salesId)
        {
            var result = new OfficalReceipt();

            using (var ctx = new posDataContext()) 
            {
                var isSalesTendered = ctx.TrnCollections.Any(x => x.SalesId == salesId);

                if (isSalesTendered)
                {
                    var saleHeader = ctx.TrnSales.FirstOrDefault(x => x.Id == salesId);
                    var collectionInfo = ctx.TrnCollections.FirstOrDefault(x => x.SalesId == salesId);
                    var saleLines = ctx.TrnSalesLines.Where(x => x.SalesId == salesId);

                    result.ORNumber = collectionInfo.CollectionNumber;
                    result.UpdateDateTime = collectionInfo.UpdateDateTime.ToString("MM/dd/yyyy hh:mm tt");
                    result.Customer = saleHeader.MstCustomer.Customer;
                    result.Terminal = saleHeader.MstTerminal.Terminal;

                    if (saleHeader.Remarks == "NA")
                    {
                        result.Remarks = "";
                    }
                    else 
                    {
                        result.Remarks = saleHeader.Remarks;
                    }
                    
                    result.LineItems = new List<LineItem>();

                    foreach (var line in saleLines) 
                    {
                        var priceDescription = "";

                        if ((line.Price - line.NetPrice) == 0)
                        {
                            priceDescription = $"{line.MstUnit.Unit} @ P{Math.Round(line.Price, 2)}";
                        }
                        else 
                        {
                            priceDescription = $"{line.MstUnit.Unit} @ P{Math.Round(line.Price, 2)} Less: P{Math.Round(line.Price - line.NetPrice, 2)} - {line.MstTax.Tax}";
                        }

                        result.LineItems.Add(new LineItem() 
                        {
                            ItemDescription = line.MstItem.ItemDescription,
                            Quantity = $"{string.Format("{0:N2}", Math.Round(line.Quantity, 2))}",
                            Amount = $"{string.Format("{0:N2}", Math.Round(line.Amount, 2))}",
                            PriceDescription = priceDescription,
                        });
                    }

                    result.TotalSales = $"₱{string.Format("{0:N2}", Math.Round(saleLines.Sum(x => x.Amount), 2))}";
                    result.TotalDiscount = $"₱{string.Format("{0:N2}", Math.Round(saleLines.Sum(x => x.DiscountAmount), 2))}";

                    var collections = ctx.TrnCollectionLines.Where(x => x.CollectionId == ctx.TrnCollections.FirstOrDefault(y => y.SalesId == salesId).Id);

                    result.TenderLines = new List<TenderLine>();

                    foreach (var collection in collections) 
                    {
                        if (collection.Amount > 0) 
                        {
                            result.TenderLines.Add(new TenderLine()
                            {
                                PayType = collection.MstPayType.PayType,
                                Amount = $"{string.Format("{0:N2}", Math.Round(collection.Amount, 2))}"
                            });
                        }
                    }

                    result.ChangeAmount = $"₱{string.Format("{0:N2}", Math.Round(ctx.TrnCollections.FirstOrDefault(y => y.SalesId == salesId).ChangeAmount, 2))}";

                    var taxes = saleLines.GroupBy(x => x.MstTax.Tax)
                        .Select(x => new 
                        {
                            Tax = x.Key, 
                            Amount = x.Sum(a => a.Amount) - x.Sum(a => a.TaxAmount), 
                            TaxAmount = x.Sum(a => a.TaxAmount)
                        });

                    result.VatLines = new List<VatLine>();

                    foreach (var tax in taxes) 
                    {
                        result.VatLines.Add(new VatLine() 
                        {
                            Tax = tax.Tax,
                            AmountLessTax = $"{string.Format("{0:N2}", Math.Round(tax.Amount, 2))}",
                            TotalTaxAmount = $"{string.Format("{0:N2}", Math.Round(tax.TaxAmount, 2))}"
                        });
                    }

                    result.SeniorCitizenDetail = new SeniorCitizen()
                    {
                        SeniorCitizenId = saleHeader.SeniorCitizenId,
                        Name = saleHeader.SeniorCitizenName,
                        Age = saleHeader.SeniorCitizenAge.GetValueOrDefault(),

                    };
                }
            }

            return result;
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