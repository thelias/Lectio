using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectioService
{
    public static class Constants
    {
        public const string AmazonS3AccessKey = "---------";
        public const string AmazonS3SecretKey = "-------------";
        public const string AmazonS3BaseUrl = "https://s3.amazonaws.com/";
        public const string AmazonS3BucketName = "lectiostorage";

        public static string GenerateUrl(string route)
        {
            return AmazonS3BaseUrl + AmazonS3BucketName + "/" + route;
        }
    }
}
