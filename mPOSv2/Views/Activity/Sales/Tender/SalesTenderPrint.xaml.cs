using mPOS.POCO.Report;
using mPOSv2.Services;
using SampleBrowser.Core;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
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

                document.PageSettings.Margins.All = 0;
                document.PageSettings.Size = PdfPageSize.A6; //new SizeF(80, 120);

                var page = document.Pages.Add();
                var graphics = page.Graphics;

                var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

                var stream = new MemoryStream();

                document.Save(stream);
                document.Close(true);

                DependencyService.Get<ISave>().Save("Official Receipt.pdf", "application/pdf", stream);

                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }            
        }
    }
}