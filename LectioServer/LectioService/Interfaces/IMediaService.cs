using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LectioService.Interfaces
{
    public interface IMediaService
    {
        Object Get(string url);
        Task<string> Insert(HttpPostedFileWrapper file, string fileName, string containerName);
        Task<bool> Delete(string url);
    }
}
