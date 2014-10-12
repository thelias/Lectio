using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace LectioTranscoder.Interfaces
{
    public interface ITranscoder
    {
        Task<Dictionary<string, MemoryStream>> TranscodeToMP4(HttpPostedFileWrapper file, string filename);
    }
}
