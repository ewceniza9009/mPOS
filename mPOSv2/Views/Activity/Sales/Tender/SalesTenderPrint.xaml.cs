using mPOS.POCO.Report;
using mPOSv2.Services;
using SampleBrowser.Core;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mPOSv2.Views.Activity.Sales
{
    public partial class SalesTenderPrint : SampleView
    {
        private const int ROW_HEIGHT = 10;

        public SalesTenderPrint()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var saleID = ((Parent as StackLayout).Parent as SalesTenderPrintContainer).SaleId;

            if (saleID != 0) 
            {
                var or = new OfficalReceipt();

                Task.Run(async () =>
                {
                    or = await APIOfficialReceipt.GetOfficialReceipt(saleID);
                    
                }).Wait();

                var document = new PdfDocument();

                document.PageSettings.Margins.All = 2;
                document.PageSettings.Size = new SizeF(150, 1000); //PdfPageSize.A6;

                var page = document.Pages.Add();
                var graphics = page.Graphics;

                var font = new PdfStandardFont(PdfFontFamily.Helvetica, 8f);

                //OR Header
                var orHeader = new PdfGrid();
                var orHeaderData = new List<object>();

                orHeaderData.Add(new { Col1 = "Official Receipt", Col2 = "" });
                orHeaderData.Add(new { Col1 = or.ORNumber, Col2 = "" });
                orHeaderData.Add(new { Col1 = or.UpdateDateTime, Col2 = "" });
                orHeaderData.Add(new { Col1 = or.Remarks, Col2 = "" });

                orHeader.DataSource = orHeaderData;

                orHeader.Headers.Clear();

                var headerMember1 = orHeader.Rows[0].Cells[0];
                headerMember1.ColumnSpan = 2;
                headerMember1.StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                headerMember1.Style.Borders.All = PdfPens.Transparent;
                orHeader.Rows[0].Height = ROW_HEIGHT;
                orHeader.Rows[0].Style = new PdfGridRowStyle() { Font = new PdfStandardFont(PdfFontFamily.Helvetica, 8f, PdfFontStyle.Bold) };

                var headerMember2 = orHeader.Rows[1].Cells[0];
                headerMember2.ColumnSpan = 2;
                headerMember2.StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                headerMember2.Style.Borders.All = PdfPens.Transparent;
                orHeader.Rows[1].Height = ROW_HEIGHT;
                orHeader.Rows[1].Style = new PdfGridRowStyle() { Font = new PdfStandardFont(PdfFontFamily.Helvetica, 8f, PdfFontStyle.Bold) };

                var headerMember3 = orHeader.Rows[2].Cells[0];
                headerMember3.ColumnSpan = 2;
                headerMember3.StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                headerMember3.Style.Borders.All = PdfPens.Transparent;
                orHeader.Rows[2].Height = ROW_HEIGHT;

                var headerMember4 = orHeader.Rows[3].Cells[0];
                headerMember4.ColumnSpan = 2;
                headerMember4.StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                headerMember4.Style.Borders.All = PdfPens.Transparent;
                orHeader.Rows[3].Height = ROW_HEIGHT;

                orHeader.Draw(page, new PointF(0, 0));

                //OR Line Items
                var orLineItems = new PdfGrid();
                var orLineItemsData = new List<object>();

                orLineItems.Columns.Add(2);

                foreach (var line in or.LineItems) 
                {
                    orLineItemsData.Add(new { Col1 = line.ItemDescription, Col2 = line.Amount });
                    orLineItemsData.Add(new { Col1 = line.Quantity + " " + line.PriceDescription, Col2 = "" });
                }

                orLineItems.DataSource = orLineItemsData;

                orLineItems.Headers.Clear();

                orLineItems.Headers.Add(1);

                orLineItems.Headers[0].Cells[0].Value = "Item";
                orLineItems.Headers[0].Cells[0].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                orLineItems.Headers[0].Cells[1].Value = "Amount";
                orLineItems.Headers[0].Cells[1].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };

                foreach (var lineItemsMember in orLineItems.Rows) 
                {
                    if (orLineItems.Rows.IndexOf(lineItemsMember) % 2 == 0)
                    {
                        lineItemsMember.Cells[0].Style.Borders.All = PdfPens.Transparent;
                        lineItemsMember.Cells[1].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Right, LineAlignment = PdfVerticalAlignment.Middle };
                        lineItemsMember.Cells[1].Style.Borders.All = PdfPens.Transparent;
                    }
                    else 
                    {
                        lineItemsMember.Cells[0].ColumnSpan = 2;
                        lineItemsMember.Cells[0].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                        lineItemsMember.Cells[0].Style.Borders.All = PdfPens.Transparent;
                    }

                    orLineItems.Rows[orLineItems.Rows.IndexOf(lineItemsMember)].Height = ROW_HEIGHT;
                }
                
                orLineItems.Columns[0].Width = 90f;

                orLineItems.Draw(page, new PointF(0, (orHeader.Rows.Count * ROW_HEIGHT) + 10));

                page.Graphics.DrawLine(new PdfPen(PdfBrushes.Black),
                    new PointF(0, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count) * ROW_HEIGHT) + 15),
                    new PointF(150, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count) * ROW_HEIGHT) + 15));

                //OR Line footer
                var orORFooter = new PdfGrid();
                var orORFooterData = new List<object>();

                orORFooterData.Add(new { Col1 = "Total Sales", Col2 = or.TotalSales });
                orORFooterData.Add(new { Col1 = "Total Discount", Col2 = or.TotalDiscount });

                orORFooter.DataSource = orORFooterData;

                orORFooter.Headers.Clear();

                orORFooter.Rows[0].Cells[0].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Left, LineAlignment = PdfVerticalAlignment.Middle };
                orORFooter.Rows[0].Cells[0].Style.Borders.All = PdfPens.Transparent;
                orORFooter.Rows[0].Cells[1].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Right, LineAlignment = PdfVerticalAlignment.Middle };
                orORFooter.Rows[0].Cells[1].Style.Borders.All = PdfPens.Transparent;
                orORFooter.Rows[0].Height = ROW_HEIGHT;
                orORFooter.Rows[0].Style = new PdfGridRowStyle() { Font = new PdfStandardFont(PdfFontFamily.Helvetica, 8f, PdfFontStyle.Bold) };

                orORFooter.Rows[1].Cells[0].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Left, LineAlignment = PdfVerticalAlignment.Middle };
                orORFooter.Rows[1].Cells[0].Style.Borders.All = PdfPens.Transparent;
                orORFooter.Rows[1].Cells[1].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Right, LineAlignment = PdfVerticalAlignment.Middle };
                orORFooter.Rows[1].Cells[1].Style.Borders.All = PdfPens.Transparent;
                orORFooter.Rows[1].Height = ROW_HEIGHT;

                orORFooter.Columns[0].Width = 90f;

                orORFooter.Draw(page, new PointF(0, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count) * ROW_HEIGHT) + 15));


                //OR Pay Lines
                var orPayItems = new PdfGrid();
                var orPayItemsData = new List<object>();

                orPayItems.Columns.Add(2);

                foreach (var line in or.TenderLines)
                {
                    orPayItemsData.Add(new { Col1 = line.PayType, Col2 = line.Amount });
                }

                orPayItemsData.Add(new { Col1 = "Change", Col2 = or.ChangeAmount });

                orPayItems.DataSource = orPayItemsData;

                orPayItems.Headers.Clear();

                foreach (var payItemsMember in orPayItems.Rows) 
                {
                    payItemsMember.Cells[0].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Right, LineAlignment = PdfVerticalAlignment.Middle };
                    payItemsMember.Cells[0].Style.Borders.All = PdfPens.Transparent;
                    payItemsMember.Cells[1].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Right, LineAlignment = PdfVerticalAlignment.Middle };
                    payItemsMember.Cells[1].Style.Borders.All = PdfPens.Transparent;

                    orPayItems.Rows[orPayItems.Rows.IndexOf(payItemsMember)].Height = ROW_HEIGHT;
                }

                page.Graphics.DrawLine(new PdfPen(PdfBrushes.Black),
                    new PointF(0, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count + orPayItems.Rows.Count) * ROW_HEIGHT) + 15),
                    new PointF(150, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count + orPayItems.Rows.Count) * ROW_HEIGHT) + 15));


                orPayItems.Columns[0].Width = 90f;

                orPayItems.Draw(page, new PointF(0, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count + orORFooter.Rows.Count) * ROW_HEIGHT) + 15));

                page.Graphics.DrawLine(new PdfPen(PdfBrushes.Black),
                    new PointF(0, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count + orPayItems.Rows.Count + orORFooter.Rows.Count) * ROW_HEIGHT) + 15),
                    new PointF(150, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count + orPayItems.Rows.Count + orORFooter.Rows.Count) * ROW_HEIGHT) + 15));

                //Vat Header
                var orVatHeader = new PdfGrid();
                var orVatHeaderData = new List<object>();

                orVatHeader.Columns.Add(3);

                orVatHeaderData.Add(new { Col1 = "VAT Details", Col2 = "", Col3 = "" });
                orVatHeaderData.Add(new { Col1 = "Amount", Col2 = "", Col3 = "VAT" });

                orVatHeader.DataSource = orVatHeaderData;

                orVatHeader.Headers.Clear();

                var vatHeaderMember1 = orVatHeader.Rows[0].Cells[0];
                vatHeaderMember1.ColumnSpan = 3;
                vatHeaderMember1.StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                vatHeaderMember1.Style.Borders.All = PdfPens.Transparent;

                orVatHeader.Rows[0].Height = ROW_HEIGHT;
                orVatHeader.Rows[0].Style = new PdfGridRowStyle() { Font = new PdfStandardFont(PdfFontFamily.Helvetica, 8f, PdfFontStyle.Bold) };

                var vatHeaderMember2 = orVatHeader.Rows[1].Cells[0];
                vatHeaderMember2.ColumnSpan = 2;
                vatHeaderMember2.StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                vatHeaderMember2.Style.Borders.All = PdfPens.Transparent;

                var vatHeaderMember3 = orVatHeader.Rows[1].Cells[2];
                vatHeaderMember3.StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Center, LineAlignment = PdfVerticalAlignment.Middle };
                vatHeaderMember3.Style.Borders.All = PdfPens.Transparent;

                orVatHeader.Rows[1].Height = ROW_HEIGHT;
                orVatHeader.Rows[1].Style = new PdfGridRowStyle() { Font = new PdfStandardFont(PdfFontFamily.Helvetica, 8f, PdfFontStyle.Bold) };

                orVatHeader.Draw(page, new PointF(0, ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count + orPayItems.Rows.Count + orORFooter.Rows.Count) * ROW_HEIGHT) + 20));

                //Vat Detail
                var orVatDetail = new PdfGrid();
                var orVatDetailData = new List<object>();

                orVatDetail.Columns.Add(3);

                foreach (var vat in or.VatLines)
                {
                    orVatDetailData.Add(new { Col1 = vat.Tax, Col2 = vat.AmountLessTax, Col3 = vat.TotalTaxAmount });
                }

                orVatDetail.DataSource = orVatDetailData;

                orVatDetail.Headers.Clear();

                foreach (var vat in orVatDetail.Rows)
                {
                    vat.Cells[0].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Left, LineAlignment = PdfVerticalAlignment.Middle };
                    vat.Cells[0].Style.Borders.All = PdfPens.Transparent;

                    vat.Cells[1].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Right, LineAlignment = PdfVerticalAlignment.Middle };
                    vat.Cells[1].Style.Borders.All = PdfPens.Transparent;

                    vat.Cells[2].StringFormat = new PdfStringFormat() { Alignment = PdfTextAlignment.Right, LineAlignment = PdfVerticalAlignment.Middle };
                    vat.Cells[2].Style.Borders.All = PdfPens.Transparent;

                    orVatDetail.Rows[orVatDetail.Rows.IndexOf(vat)].Height = ROW_HEIGHT;
                }

                orVatDetail.Draw(page, new PointF(0, 
                    ((orHeader.Rows.Count + orLineItems.Headers.Count + orLineItems.Rows.Count + orPayItems.Rows.Count + orORFooter.Rows.Count + orVatHeader.Rows.Count) * ROW_HEIGHT) + 20));

                var stream = new MemoryStream();

                document.Save(stream);
                document.Close(true);

                DependencyService.Get<ISave>().Save("Official Receipt.pdf", "application/pdf", stream);

                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }            
        }
    }
}