using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Widget;
using mPOSv2.Android;
using mPOSv2.Custom;
using Syncfusion.XForms.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AutoSizableLabel), typeof(AutoSizableLabelRenderer))]
namespace mPOSv2.Android
{
    public class AutoSizableLabelRenderer : LabelRenderer
    {
        private readonly int MAX_LINE_LENGTH = 18;

        public AutoSizableLabelRenderer(Context context) : base(context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null || !(e.NewElement is AutoSizableLabel autoLabel) || Control == null)
            {
                return; 
            }

            Control.SetAutoSizeTextTypeWithDefaults(AutoSizeTextType.Uniform);

            if (!string.IsNullOrEmpty(autoLabel.Text))
            {
                double numberOfLines = Math.Ceiling((double)(autoLabel.Text.Length / MAX_LINE_LENGTH));

                autoLabel.MaxLines = 3;
                autoLabel.HeightRequest = autoLabel.HeightRequest * (numberOfLines > 3 ? 3 : numberOfLines) ;
            }                       
        }
    }
}