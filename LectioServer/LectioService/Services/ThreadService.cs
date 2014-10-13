using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectioService.Interfaces;
using LectioService.Entities;

namespace LectioService.Services
{
    public class ThreadService : IThreadService
    {

        private readonly LectioContext _context;

        public ThreadService(LectioContext context)
        {
            _context = context;
        }

        public Thread GetThread(Video video, ApplicationUser user, int threadId)
        {
            var vid = user.Videos.SingleOrDefault(x => x.VideoId == video.VideoId);
            if (vid == null)
            {
                throw new Exception("Access Denied");
            }

            if(threadId == vid.ThreadId)
            {
                return vid.Thread;
            }

            throw new Exception("Thread Not Found");
        }
      

        public void AddNewThread(ApplicationUser user, Thread thread, Video video)
        {
            var vid = user.Videos.SingleOrDefault(x => x.VideoId == video.VideoId);
            if (vid == null)
            {
                throw new Exception("Access Denied");
            }
        
            if(vid.Thread != null)
            {
                throw new Exception("Thread Already Exists");
            }

            vid.Thread = thread;
            _context.SaveChanges();
        }

    }
}
