using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Amazon;
using Amazon.EC2.Model;
using Amazon.S3;
using Amazon.S3.Model;
using LectioService.Entities;
using LectioService.Interfaces;

namespace LectioService.Services
{
    public class MediaService : IMediaService
    {
        private readonly string bucketName = "lectiobucket";
        private readonly IThumbnailService _thumbnailService = new ThumbnailService();
        private IAmazonS3 client;
        public object Get(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<Video> UploadVideoAsync(HttpPostedFileWrapper file, string fileName, string containerName)
        {
            if (file == null || file.ContentLength <= 0) 
                throw new ArgumentNullException("file");
            if (!Regex.IsMatch(file.FileName, @"^.*\.(mp4|MP4)$")) 
                throw new ArgumentException("Invalid image type");
            var ext = file.FileName.Split('.').Last();
            var imageName = "";
            var imageContainerName = containerName;
            containerName = containerName.ToLower();

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
            System.Drawing.Image image = await _thumbnailService.ExtractThumbnailAsync(file, ext);
            var imageUrl = await UploadThumbnailAsync(image, imageName, imageContainerName);

            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USWest2))
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    ContentType = "video/" + ext,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = file.InputStream
                };

                var response = client.PutObject(request);
            }
            return null;
        }

        public Task<string> UploadThumbnailAsync(System.Drawing.Image thumbnail, string fileName, string containerName)
        {

            var ext = fileName.Split('.').Last();

            containerName = containerName.ToLower();

            fileName = fileName.Split('/').Last().Split('.').First() + "_" + Guid.NewGuid() + "." + ext;
            
            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USWest2))
            {
                using (var memoryStream = new MemoryStream())
                {
                    thumbnail.Save(memoryStream, ImageFormat.Png);
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = fileName,
                        ContentType = "image/" + ext,
                        CannedACL = S3CannedACL.PublicRead,
                        InputStream = memoryStream
                    };

                    var response = client.PutObject(request);
                }
            }
            return null;
        }

        public async Task<bool> DeleteVideoAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException("url");

            var split = url.Split('/');
            if (split.Length < 3) throw new ArgumentException("Url is malformed");

            var fileName = split[split.Length - 1];
            var containerName = split[split.Length - 2];

            var deleteObjectRequest =
                new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USWest2))
            {
                client.DeleteObject(deleteObjectRequest);
            }

            return true;
        }

        public Task<bool> DeleteThumbnailAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}
