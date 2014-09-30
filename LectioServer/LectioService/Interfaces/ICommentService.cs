using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectioService.Entities;

namespace LectioService.Interfaces
{
    interface ICommentService
    {

        List<Comment> GetComments(Video video, int pg, int num);

        void AddNewComment(ApplicationUser user, Comment comment, Video video);
    }
}
