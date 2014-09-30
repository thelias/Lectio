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
using LectioService;
using LectioService.Entities;

namespace LectioServer.Controllers.Api.V1
{
    [Authorize]
    [RoutePrefix("api/v1/lectures")]
    public class LecturesController : ApiController
    {
        private readonly LectioContext _context;
        public LecturesController()
        {
            _context = new LectioContext();
        }

        public IHttpActionResult GetLectures(int pg, int num)
        {
            // todo: finish method
            return Ok();
        }

        public IHttpActionResult AddLectures()
        {
            // todo: finish method
            return Ok();
        }

        public IHttpActionResult SelectLecture(int lectureId)
        {
            // todo: finish method
            return Ok();
        }

        public IHttpActionResult DeleteLecture(int lectureId)
        {
            // todo: finish method
            return Ok();
        }

        public IHttpActionResult UpdateLecture(Lecture lecture)
        {
            // todo: finish method
            return Ok();
        }
    }
}
