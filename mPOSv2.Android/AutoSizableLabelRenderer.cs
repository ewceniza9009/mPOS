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
using mPOSv2.CustomControls;
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
        private const int MAX_LINE_COUNT = 2;
        private const int MAX_LINE_LENGTH = 20;
        private const int TEXT_HEIGHT = 18;

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

            //Control.SetAutoSizeTextTypeWithDefaults(AutoSizeTextType.Uniform);

            if (!string.IsNullOrEmpty(autoLabel.Text))
            {
                double numberOfLines = Math.Ceiling((double)((double)(autoLabel.Text.Length < MAX_LINE_LENGTH ? MAX_LINE_LENGTH : autoLabel.Text.Length) / MAX_LINE_LENGTH));

                autoLabel.MaxLines = MAX_LINE_COUNT;
                autoLabel.HeightRequest = TEXT_HEIGHT * (numberOfLines > MAX_LINE_COUNT ? MAX_LINE_COUNT : numberOfLines) ;
            }                       
        }
    }
}