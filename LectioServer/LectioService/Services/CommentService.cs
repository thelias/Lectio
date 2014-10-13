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
            throw new NotImplementedException(); //TODO: FIX!!!
        }

        public void AddNewComment(ApplicationUser user, Comment comment, Video video)
        {
            

            throw new NotImplementedException(); //TODO: FIX

            
        }
    }
}
