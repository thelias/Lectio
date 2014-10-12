using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LectioService.Entities;

namespace LectioService.Interfaces
{
    public interface IAmazonService
    {
        object Get(string url);

        Task<Video> UploadVideo(HttpPostedFileWrapper file, string filename);

        Task<string> UploadImage(HttpPostedFileWrapper file, string filename, string containerName);

        void Delete(string url);
    }
}
