/*
 * Author:
 * Ian Jones
 */

using System.Collections.Generic;
using LectioService.Entities;

namespace LectioService.Interfaces
{
    public interface ICommentService
    {

        List<Comment> GetComments(Video video, int pg, int num);

        void AddNewComment(ApplicationUser user, Comment comment, Video video);
    }
}
