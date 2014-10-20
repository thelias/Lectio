using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LectioService;
using LectioService.Entities;
using LectioService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LectioTest
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void AddNewLectureTest()
        {
            var context = new LectioContext();
            var lectureService = new LectureService(context);

            var user = context.Users.Single(x => x.UserName == "czifro");
            var oldLectureCount = user.Lectures.Count();
            var lecture = new Lecture
            {
                LectureName = "Czifro's CS Course"
            };

            lectureService.AddNewLecture(user, lecture);

            var modUser = context.Users.Single(x => x.Id == user.Id);
            Assert.AreNotEqual(oldLectureCount, modUser.Lectures.Count);
        }

        [TestMethod]
        public void UploadNewVideo()
        {
            var context = new LectioContext();
            var lectureService = new LectureService(context);
            var videoService = new VideoService(context);

            var user = context.Users.Single(x => x.UserName == "czifro");
            var lecture = lectureService.GetLecture(1);
            var videoCount = lecture.Videos == null ? 0 : lecture.Videos.Count;

            var video = new Video
            {
                Thread = new Thread(),
                ThumbnailUrl = Constants.GenerateUrl(""),
                VideoUrl = Constants.GenerateUrl("")
            };

            videoService.AddNewVideo(user, lecture, video);

            var modLecture = lectureService.GetLecture(lecture.LectureId);

            Assert.AreNotEqual(videoCount, modLecture.Videos.Count);
        }
    }
}
