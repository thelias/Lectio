using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LectioService;
using LectioService.Interfaces;
using LectioService.Services;

namespace LectioServer.Controllers.Api.V1
{
    public class VideoController : ApiController
    {
        private readonly LectioContext _context;
        private readonly IVideoService _videoService;
        private readonly ILectureService _lectureService;

        public VideoController()
        {
            _context = new LectioContext();
            _videoService = new VideoService(_context);
            _lectureService = new LectureService(_context);
        }

        public IHttpActionResult GetVideos(int lectureid, int pg, int num)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var lecture = _lectureService.GetLecture(lectureid);
            var videos = _videoService.GetVideos(user, lecture, pg, num);
            return Ok(videos);
        }

        public IHttpActionResult GetVideo(int videoid)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var vid = _videoService.GetVideo(user, videoid);
            return Ok(vid);
        }

        public IHttpActionResult UploadVideo()
        {
            return Ok();
        }
    }
}
