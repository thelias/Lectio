/*
 * Author:
 * Will Czifro
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectioService
{
    public static class Constants
    {
        public const string AmazonS3AccessKey = "AKIAIPOJWHFHERJBJM5Q";
        public const string AmazonS3SecretKey = "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";
        public const string AmazonS3BaseUrl = "https://s3.amazonaws.com/";
        public const string AmazonS3BucketName = "lectiooutput";
        public const string AmazonS3TempBucket = "lectioinput";

        public static string GenerateUrl(string route)
        {
            return AmazonS3BaseUrl + AmazonS3BucketName + "/" + route;
        }
    }
}
