/*
 * Author:
 * Ian Jones
 */

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
    [RoutePrefix("api/v1/lectures")]
    public class LecturesController : ApiController
    {
        private readonly LectioContext _context;
        private readonly ILectureService _lectureService;
        public LecturesController()
        {
            _context = new LectioContext();
            _lectureService = new LectureService(_context);
        }

        [HttpGet]
        [Route("GetLecture")]
        public IHttpActionResult GetLecture(int lectureId)
        {
            var lecture = _lectureService.GetLecture(lectureId);
            return Ok(lecture);
        }

        [HttpGet]
        [Route("GetLectures")]
        public IHttpActionResult GetLectures(int pg, int num)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var results = _lectureService.GetLectures(user, pg, num);
            return Ok(results);
        }

        [HttpPost]
        [Route("AddLectures")]
        public IHttpActionResult AddLectures(LectureModel model)
        {
            var lecture = new Lecture {LectureName = model.LectureName};
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            _lectureService.AddNewLecture(user, lecture);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteLecture")]
        public IHttpActionResult DeleteLecture(int lectureId)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            var lecture = _context.Lectures.SingleOrDefault(x => x.LectureId == lectureId);
            if (lecture == null)
                return NotFound();
            _lectureService.DeleteLecture(user, lecture);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateLecture")]
        public IHttpActionResult UpdateLecture(Lecture lecture)
        {
            var user = _context.Users.Single(x => x.UserName == User.Identity.Name);
            _lectureService.UpdateLecture(user, lecture);
            return Ok();
        }
    }
}
