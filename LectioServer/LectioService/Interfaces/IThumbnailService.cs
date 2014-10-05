using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LectioService.Interfaces
{
    public interface IThumbnailService
    {
        Task<Image> ExtractThumbnailAsync(HttpPostedFileWrapper file, string ext);

        Task<bool> DeleteTempFileAsync(string path);
    }
}
