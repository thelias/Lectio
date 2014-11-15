/*
 * Author:
 * Will Czifro
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using LectioServer.Models;
using LectioService;
using LectioService.Entities;
using LectioService.Interfaces;
using LectioService.Services;
//using LectioTranscoder;

namespace LectioServer.Controllers.Api.V1
{
    [Authorize]
    [RoutePrefix("api/v1/video")]
    public class VideoController : ApiController
    {
        private readonly LectioContext _context;
        private readonly IVideoService _videoService;
        private readonly ILectureService _lectureService;
        private readonly IAmazonService _amazonService;

        public VideoController()
        {
            _context = new LectioContext();
            _videoService = new VideoService(_context);
            _lectureService = new LectureService(_context);
            _amazonService = new AmazonService();
        }

        [HttpGet]
        [Route("GetVideos")]
        [ResponseType(typeof(List<Video>))]
        public IHttpActionResult GetVideos(int lectureid, int pg, int num)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var lecture = _lectureService.GetLecture(lectureid);
            var videos = _videoService.GetVideos(user, lecture, pg, num);
            return Ok(videos);
        }

        [HttpGet]
        [Route("Getvideo")]
        [ResponseType(typeof(Video))]
        public IHttpActionResult GetVideo(int videoid)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var vid = _videoService.GetVideo(user, videoid);
            return Ok(vid);
        }

        [HttpPost]
        [Route("UploadVideo")]
        public async Task<IHttpActionResult> UploadVideo(VideoModel model)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count <= 0)
            {
                return BadRequest("No file");
            }

            var file = new HttpPostedFileWrapper(httpRequest.Files[0]);

            var video = await _amazonService.UploadVideo(file, file.FileName);

            video.VideoName = ""; // todo: remove this later

            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);

            var lecture = _lectureService.GetLecture(model.LectureId);

            _videoService.AddNewVideo(user, lecture, video);

            return Ok("Processing video");
        }

        [HttpPost]
        [Route("TestUpload")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> TestUpload()
        {
            //var httpRequest = HttpContext.Current.Request;
            //if (httpRequest.Files.Count <= 0)
            //{
            //    return BadRequest("No file");
            //}

            //var file = new HttpPostedFileWrapper(httpRequest.Files[0]);

            //var transcoder = new Transcoder();
            //var r = await transcoder.TranscodeToMP4(file, file.FileName);
            //var rr = r;
            return Ok("Testing Transcoder");
        }

        [HttpGet]
        [Route("TestAmazonTranscoder")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> TestAmazonTranscoder()
        {

            var filename = Constants.GenerateUrl("clipcanvas_14348_offline_199deb03-6202-4c28-a6c1-be5485fa134a.mp4");

            await _amazonService.CreateTranscodingJobAsync("clipcanvas_14348_offline_199deb03-6202-4c28-a6c1-be5485fa134a.mp4");

            return Ok("Testing Amazon Transcoder");
        }
    }
}
