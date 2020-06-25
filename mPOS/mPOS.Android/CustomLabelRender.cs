using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using mPOS.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.FastRenderers;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace mPOS.Droid
{
    public class CustomLabelRenderer : LabelRenderer
    {
        public CustomLabelRenderer(Context context) : base(context)
        {

        }

        protected override void OnAttachedToWindow()
        {
            try
            {
                base.OnAttachedToWindow();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}