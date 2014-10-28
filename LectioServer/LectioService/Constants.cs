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
        #region Database Constants
        public static string StudentRoleId;
        public static string InstructorRoleId;
        #endregion

        #region Amazon Constants
        public const string AmazonS3AccessKey = "AKIAIPOJWHFHERJBJM5Q";
        public const string AmazonS3SecretKey = "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";
        public const string AmazonS3BaseUrl = "https://s3.amazonaws.com/";
        public const string AmazonS3BucketName = "lectiostorage";
        #endregion

        public static string GenerateUrl(string route)
        {
            return AmazonS3BaseUrl + AmazonS3BucketName + "/" + route;
        }

        public static void LoadConstants(LectioContext context)
        {
            //StudentRoleId = context.Roles.Single(r => r.Name == "Student").Id;
            //InstructorRoleId = context.Roles.Single(r => r.Name == "Instructor").Id;
        }
    }
}
