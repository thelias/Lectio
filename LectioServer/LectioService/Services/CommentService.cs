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
            var list = video.Comments.Skip(pg*num).Take(num).ToList();
            return list;
        }

        public void AddNewComment(ApplicationUser user, Comment comment, Video video)
        {
            if(video.UserId != user.Id) return;


            if (video.Comments == null || !video.Comments.Any())
            {
                video.Comments = new Collection<Comment>();
            }

            video.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
