/*
 * Author:
 * Ian Jones,
 * Jordanne Perry
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LectioService;
using LectioService.Entities;
using LectioService.Interfaces;
using LectioService.Services;
using LectioServer.Models;

namespace LectioServer.Controllers.Api.V1
{
    [Authorize]
    [RoutePrefix("api/v1/comments")]
    public class CommentsController : ApiController
    {
        private readonly LectioContext _context;
        private readonly ICommentService _commentService;
        private readonly IVideoService _videoService;

        public CommentsController() 
        { 
            _context = new LectioContext();
            _commentService = new CommentService(_context);
            _videoService = new VideoService(_context);
        }

        [HttpGet]
        [Route("GetComments")]
        public IHttpActionResult GetComments(int videoid, int pg, int num) 
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var video = _videoService.GetVideo(user, videoid);
            var comment = _commentService.GetComments(video, pg, num);
            return Ok(comment);
        }

        [HttpPost]
        [Route("Uploadcomment")]
        public IHttpActionResult AddComment(CommentModel model)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var comment = new Comment { UserId = user.Id, CommentText = model.CommentText };
            var video = _videoService.GetVideo(user, model.VideoId);
            _commentService.AddNewComment(user, comment, video);
            return Ok();
        }
    }
}
