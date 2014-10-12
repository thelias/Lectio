using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LectioTranscoder.Interfaces;
using LibImages;
using LibMPlayerCommon;
using Microsoft.WindowsAPICodePack.Shell;

namespace LectioTranscoder
{
    /// <summary>
    /// Takes an uploaded video wrapped by HttpPostedFileWrapper and transcodes it to MP4
    /// Note: This requires use of storage, files must be saved and read to transcode.
    ///       This class cleans up any temp files that it saves.
    /// </summary>
    public class Transcoder : ITranscoder
    {
        public async Task<Dictionary<string, MemoryStream>> TranscodeToMP4(HttpPostedFileWrapper file, string filename)
        {
            var results = new Dictionary<string, MemoryStream>();
            var videostream = new MemoryStream();
            var newFilename = filename.Replace(filename.Split('.').Last(), "mp4");
            var path = "/../TempStorage/";
            bool error = false, completeFail = false;
            // Temporarily save video file
            file.SaveAs(path+filename);
            // Attempt to tanscode file
            try
            {
                // uses MPlayer library to transcode file to MP4
                var mencoder = new Mencoder();
                mencoder.Convert(Mencoder.VideoType.mpeg4, Mencoder.AudioType.mp3, path+filename, path+newFilename);

                // Read in new video and copy to MemoryStream object
                var filestream = File.Open(path + filename, FileMode.Open);
                filestream.Position = 0;
                filestream.CopyTo(videostream);

                // add MemoryStream object to dictionary and get a thumbnail MemoryStream object
                results.Add("videostream", videostream);
                results.Add("thumbstream", await ExtractThumbnailAsync(path+newFilename));
            }
            catch (Exception)
            {
                error = true;  // run error code
            }

            if (error)
            {
                // We failed, let's clean up and leave
                Task.Run(async () => await CleanupFileAsync(filename));
                throw new Exception("Failed to transcode video");
            }

            // We succeeded, let's clean up and return results
            await CleanupFileAsync(filename);
            return results;
        }

        /// <summary>
        /// Reads in file and extracts thumbnail then deletes video file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private async Task<MemoryStream> ExtractThumbnailAsync(string filename)
        {
            // create MemoryStream object to store thumbnail
            var thumbstream = new MemoryStream();

            // open up file and extract thumbnail
            var f = ShellFile.FromFilePath(filename);
            var bitmap = f.Thumbnail.ExtraLargeBitmap;

            // convert to Image object and save to Memory stream object
            System.Drawing.Image image = bitmap;
            image.Save(thumbstream, ImageFormat.Png);

            // we succeeded, let's cleanup and return MemoryStream object
            await CleanupFileAsync(filename);
            return thumbstream;
        }

        /// <summary>
        /// Needs to be implemented, this is a fallback in case the first method fails
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Task<MemoryStream> TranscodeFallbackAsync(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Needs to be implemented, this is a fallback in case the first method fails
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Task<MemoryStream> ExtractThumbnailFallbackAsync(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cleans up temporary files created during transcoding
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Task<int> CleanupFileAsync(string filename)
        {
            File.Delete(filename);
            return Task.FromResult(0);
        }
    }
}
