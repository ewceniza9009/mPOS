using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using Android.Support.V4.Content;
using SampleBrowser.PDF.Droid;
using mPOSv2.Services;
using mPOSv2.Android;

[assembly: Dependency(typeof(SaveAndroid))]
[assembly: Dependency(typeof(MailService))]
namespace SampleBrowser.PDF.Droid
{
    internal class SaveAndroid : ISave
    {
        public void Save(string fileName, string contentType, MemoryStream stream)
        {
            var context = MainActivity.Instance;
            string root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists())
            {
                file.Delete();
            }

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }

            if (file.Exists() && contentType != "application/html")
            {
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".provider", file);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);

                context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }
        }
    }

    public class MailService : IMailService
    {
        public MailService()
        {
        }

        public void ComposeMail(string fileName, string[] recipients, string subject, string messagebody, MemoryStream filestream)
        {
            var context = MainActivity.Instance;
            string root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists())
            {
                file.Delete();
            }

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(filestream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                _ = e.ToString();
            }

            Intent email = new Intent(Android.Content.Intent.ActionSend);
            Android.Net.Uri uri = FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".provider", file);
            email.PutExtra(Android.Content.Intent.ExtraSubject, subject);
            email.PutExtra(Intent.ExtraStream, uri);
            email.SetType("application/pdf");
            context.StartActivity(email);
        }
    }
}