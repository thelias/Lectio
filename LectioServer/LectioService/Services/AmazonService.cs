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
//using LectioTranscoder;
//using LectioTranscoder.Interfaces;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;

namespace LectioService.Services
{
    public class AmazonService : IAmazonService
    {
        private IAmazonElasticTranscoder transcoder;
        private IAmazonS3 client;
        //private ITranscoder transcoder = new Transcoder();
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
                fileName = fileName.Split('/').Last().Split('.').First() + "_" + Guid.NewGuid() + "." + ext;

            }

            //var processedVideoResults = await transcoder.TranscodeToMP4(file, file.FileName);

            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USEast1))
            {
                var request = new PutObjectRequest
                {
                    BucketName = Constants.AmazonS3BucketName,
                    Key = fileName,
                    ContentType = "video/" + ext,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = file.InputStream//processedVideoResults["videostream"]
                };

                var response = client.PutObject(request);
            }

            var result = await CreateTranscodingJobAsync(fileName);



            client = null;
            //var thumbnailUrl = await UploadThumbnail(processedVideoResults["thumbstream"], imageName, ext);

            var video = new Video
            {
                ThumbnailUrl = Constants.GenerateUrl(fileName + "_00001.png"),
                VideoUrl = Constants.GenerateUrl(fileName + "_enc.mp4")
            };

            return video;
        }

        private Task<string> UploadThumbnail(MemoryStream thumbstream, string imageName, string ext)
        {
            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USEast1))
            {
                var request = new PutObjectRequest
                {
                    BucketName = Constants.AmazonS3BucketName,
                    Key = imageName,
                    ContentType = "image/" + ext,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = thumbstream
                };

                var response = client.PutObject(request);
            }

            return Task.FromResult(Constants.GenerateUrl(imageName));
        }

        public async Task<int> CreateTranscodingJobAsync(string filename)
        {
            transcoder = new AmazonElasticTranscoderClient(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USEast1);

            var ext = filename.Substring('.').Last().ToString();

            var ji = new JobInput
            {
                AspectRatio = "auto",
                Container = "auto",
                FrameRate = "auto",
                Interlaced = "auto",
                Resolution = "auto",
                Key = filename
            };

            var output = new CreateJobOutput
            {
                ThumbnailPattern = filename + "_{count}",
                Rotate = "auto",
                PresetId = "1351620000001-000010",
                Key = filename + "_enc.mp4"
            };

            var createJob = new CreateJobRequest
            {
                Input = ji,
                Output = output,
                PipelineId = "1413597383537-ioc01m"
            };

            var response = await transcoder.CreateJobAsync(createJob);

            var r = response;

            return 0;
        }

        public Task<string> UploadImage(HttpPostedFileWrapper file, string filename, string containerName)
        {
            throw new NotImplementedException();
        }

        public void Delete(string filename)
        {
            using (client = new AmazonS3Client(Constants.AmazonS3AccessKey, Constants.AmazonS3SecretKey, RegionEndpoint.USEast1))
            {
                var delete = new DeleteObjectRequest
                {
                    Key = filename,
                    BucketName = Constants.AmazonS3BucketName
                };

                var response = client.DeleteObject(delete);
            }
        }
    }
}
