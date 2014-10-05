using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LectioService.Interfaces;
using Microsoft.WindowsAPICodePack.Shell;


namespace LectioService.Services
{
    public class ThumbnailService : IThumbnailService
    {
        public Task<Image> ExtractThumbnailAsync(HttpPostedFileWrapper file, string ext)
        {
            var filename = Guid.NewGuid() + "." + ext;
            file.SaveAs("/TempStorage/" + filename);

            var f = ShellFile.FromFilePath("/TempStorage/" + filename);
            var thumb = f.Thumbnail.ExtraLargeBitmap;
            Image image = thumb;

            Task.Run(() => DeleteTempFileAsync("/TempStorage/" + filename));

            return Task.FromResult(image);
        }

        public Task<bool> DeleteTempFileAsync(string path)
        {
            File.Delete(path);
            return Task.FromResult(true);
        }
    }
}
