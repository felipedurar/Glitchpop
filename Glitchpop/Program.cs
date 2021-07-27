using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glitchpop
{
    class Program
    {
        static void Main(string[] args)
        {
            // Startup Register
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            const string RegisterKeyName = "Glitchpop";

            // Check Unregister Command
            if (args.Length > 0)
                if (args[0].ToLower() == "unregister")
                {
                    rkApp.DeleteValue(RegisterKeyName, false);
                    return;
                }

            // Add to windows startup
            if (rkApp.GetValue(RegisterKeyName) == null)
            {
                rkApp.SetValue(RegisterKeyName, Assembly.GetEntryAssembly().Location);
            }

            // Output Path
            string outImgPath = Path.Combine(Path.GetTempPath(), "outGlitchpopWallpaper.jpg");

            // Delete Previous
            if (File.Exists(outImgPath))
                File.Delete(outImgPath);

            // Calculate Days
            DateTime startTime = new DateTime(2021, 03, 03);
            DateTime endDate = DateTime.Now;
            int days = (int)(endDate - startTime).TotalDays;

            // Create the Image
            Bitmap wallpaper = new Bitmap(1920, 1080);
            Graphics graphics = Graphics.FromImage(wallpaper);
            graphics.Clear(Color.Black);

            // Draw It and Save
            int cPosY = 250;
            string msg = string.Format("Já são {0} dias sem a Phantom Glitchpop!", days);
            Font font = new Font("Arial", 32);
            SizeF txtSize = graphics.MeasureString(msg, font);
            graphics.DrawString(msg, font, new SolidBrush(Color.White), new Point((int)((wallpaper.Width / 2) - (txtSize.Width / 2)), cPosY));

            cPosY += (int)(txtSize.Height + 100);
            graphics.DrawImage(Resources.phantom, new PointF((wallpaper.Width / 2) - (Resources.phantom.Width / 2), cPosY));

            msg = "Valeu Riot!";
            txtSize = graphics.MeasureString(msg, font);
            cPosY += Resources.phantom.Height + 100;
            graphics.DrawString(msg, font, new SolidBrush(Color.White), new Point((int)((wallpaper.Width / 2) - (txtSize.Width / 2)), cPosY));
            wallpaper.Save(outImgPath, ImageFormat.Jpeg);

            Wallpaper.Set(new Uri(outImgPath), Wallpaper.Style.Stretched);
        }
    }
}
