using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LectioServer.Models;
using LectioService;
using LectioService.Entities;
using LectioService.Interfaces;
using LectioService.Services;

namespace LectioServer.Controllers.Api.V1
{
    [Authorize]
    [RoutePrefix("api/v1/threads")]
    public class ThreadController : ApiController
    {
        private readonly LectioContext _context;
        private readonly IThreadService _threadService;
        private readonly IVideoService _videoService;

        public ThreadController()
        {
            _context = new LectioContext();
            _threadService = new ThreadService(_context);
            _videoService = new VideoService(_context);
        }

        [HttpGet]
        [Route("GetThread")]
        public IHttpActionResult GetThread(int videoid, int threadid)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var vid = _videoService.GetVideo(user, videoid);
            var thread = _threadService.GetThread(vid, user, threadid);
            return Ok(thread);
        }

        [HttpPost]
        [Route("PostThread")]
        public IHttpActionResult PostThread(ThreadModel threadmodel)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var vid = _videoService.GetVideo(user, threadmodel.VideoId);
            var thread = new Thread();
            _threadService.AddNewThread(user, thread, vid);
            return Ok();
        }

    }
}