using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using Android.Support.V4.Content;
using SampleBrowser.PDF.Droid;
using mPOSv2.Services;
using mPOSv2.Android;
using Android.Print;
using Android.PrintServices;

[assembly: Dependency(typeof(PrintAndroid))]
[assembly: Dependency(typeof(SaveAndroid))]
[assembly: Dependency(typeof(MailService))]
namespace SampleBrowser.PDF.Droid
{
    internal class PrintAndroid : IPrint
    {
        public void Print(string fileName, Stream inputStream)
        {
            if (inputStream.CanSeek) 
            {
                //Reset the position of PDF document stream to be printed
                inputStream.Position = 0;
            }
                
            //Create a new file in the Personal folder with the given name
            string createdFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);

            //Save the stream to the created file
            using (var dest = System.IO.File.OpenWrite(createdFilePath))
            {
                inputStream.CopyTo(dest); 
            }

            string filePath = createdFilePath;
            var activity = Xamarin.Essentials.Platform.CurrentActivity;

            PrintManager printManager = (PrintManager)activity.GetSystemService(Context.PrintService);
            PrintDocumentAdapter pda = new CustomPrintDocumentAdapter(filePath);

            //Print with null PrintAttributes
            printManager.Print(fileName, pda, null);
        }
    }

    internal class CustomPrintDocumentAdapter : Android.Print.PrintDocumentAdapter
    {
        internal string FileToPrint { get; set; }

        internal CustomPrintDocumentAdapter(string fileDesc)
        {
            FileToPrint = fileDesc;
        }

        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, Android.OS.CancellationSignal cancellationSignal, LayoutResultCallback callback, Android.OS.Bundle extras)
        {
            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();
                return;
            }

            PrintDocumentInfo pdi = new PrintDocumentInfo.Builder(FileToPrint).SetContentType(Android.Print.PrintContentType.Document).Build();

            callback.OnLayoutFinished(pdi, true);
        }

        public override void OnWrite(PageRange[] pages, Android.OS.ParcelFileDescriptor destination, Android.OS.CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            InputStream input = null;
            OutputStream output = null;

            try
            {
                //Create FileInputStream object from the given file
                input = new FileInputStream(FileToPrint);
                //Create FileOutputStream object from the destination FileDescriptor instance
                output = new FileOutputStream(destination.FileDescriptor);

                byte[] buf = new byte[1024];
                int bytesRead;

                while ((bytesRead = input.Read(buf)) > 0)
                {
                    //Write the contents of the given file to the print destination
                    output.Write(buf, 0, bytesRead);
                }

                callback.OnWriteFinished(new Android.Print.PageRange[] { Android.Print.PageRange.AllPages });

            }
            catch (Java.IO.FileNotFoundException ee)
            {
                //Catch exception
            }
            catch (Exception e)
            {
                //Catch exception
            }
            finally
            {
                try
                {
                    input.Close();
                    output.Close();
                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }
    }

    internal class SaveAndroid : ISave
    {
        public void Save(string fileName, string contentType, MemoryStream stream)
        {
            var context = MainActivity.Instance;
            string root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/FileRepos");
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

            Java.IO.File myDir = new Java.IO.File(root + "/FileRepos");
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