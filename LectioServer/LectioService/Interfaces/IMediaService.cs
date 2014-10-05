using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LectioService.Entities;

namespace LectioService.Interfaces
{
    public interface IMediaService
    {
        Object Get(string url);
        Task<Video> UploadVideoAsync(HttpPostedFileWrapper file, string fileName, string containerName);
        Task<string> UploadThumbnailAsync(System.Drawing.Image thumbnail, string fileName, string containerName); 
        Task<bool> DeleteVideoAsync(string url);
        Task<bool> DeleteThumbnailAsync(string url);
    }
}
