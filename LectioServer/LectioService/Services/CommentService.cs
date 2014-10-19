/*
 * Authors:
 * Ian Jones,
 * Jordanne Perry,
 * Will Czifro
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectioService.Entities;
using LectioService.Interfaces;

namespace LectioService.Services
{
     public class CommentService: ICommentService
    {
        private readonly LectioContext _context;

        public CommentService(LectioContext context)
        {
            _context = context;
        }

        public List<Comment> GetComments(Video video, int pg, int num)
        {
            var comments = video.Thread.Comments.Skip(pg*num)
                                                .Take(num)
                                                .ToList();
            return comments;
        }

        public void AddNewComment(ApplicationUser user, Comment comment, Video video)
        {
            var vid = user.Videos.SingleOrDefault(x => x.VideoId == video.VideoId);

            if (vid == null)
            {
                throw new Exception("Access Denied");
            }

            vid.Thread.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
