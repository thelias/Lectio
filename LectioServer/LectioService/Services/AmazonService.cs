using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using LectioService.Entities;
using LectioService.Interfaces;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using LectioTranscoder;
using LectioTranscoder.Interfaces;

namespace LectioService.Services
{
    public class AmazonService : IAmazonService
    {
        private IAmazonS3 client;
        private ITranscoder transcoder = new Transcoder();
        public object Get(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<Video> UploadVideo(HttpPostedFileWrapper file, string fileName)
        {
            if (file == null || file.ContentLength <= 0)
                throw new ArgumentNullException("file");
            //if (!Regex.IsMatch(file.FileName, @"^.*\.(mp4|MP4)$"))
            //    throw new ArgumentException("Invalid image type");
            var ext = "mp4";
            var imageName = "";

            if (fileName == null)
            {
                fileName = Guid.NewGuid() + "." + ext;
                imageName = fileName.Split('.').First() + ".png";
            }
            else
            {
                imageName = fileName.Split('/').Last().Split('.').First() + "_" + Guid.NewGuid() + ".png";
                fileName = fileName.Split('/').Last().Split('.').First() + "_" + Guid.NewGuid() + "." + ext;
            }

            var processedVideoResults = await transcoder.TranscodeToMP4(file, file.FileName);

            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USWest2))
            {
                var request = new PutObjectRequest
                {
                    BucketName = Constants.AmazonS3BucketName,
                    Key = fileName,
                    ContentType = "video/" + ext,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = processedVideoResults["videostream"]
                };

                var response = client.PutObject(request);
            }
            client = null;
            var thumbnailUrl = await UploadThumbnail(processedVideoResults["thumbstream"], imageName, ext);

            var video = new Video
            {
                ThumbnailUrl = thumbnailUrl,
                VideoUrl = Constants.GenerateUrl(fileName)
            };

            return video;
        }

        private Task<string> UploadThumbnail(MemoryStream thumbstream, string imageName, string ext)
        {
            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USWest2))
            {
                var request = new PutObjectRequest
                {
                    BucketName = Constants.AmazonS3BucketName,
                    Key = imageName,
                    ContentType = "video/" + ext,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = thumbstream
                };

                var response = client.PutObject(request);
            }

            return Task.FromResult(Constants.GenerateUrl(imageName));
        }

        public Task<string> UploadImage(HttpPostedFileWrapper file, string filename, string containerName)
        {
            throw new NotImplementedException();
        }

        public void Delete(string url)
        {
            throw new NotImplementedException();
        }
    }
}
