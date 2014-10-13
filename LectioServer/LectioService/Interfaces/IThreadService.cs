using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectioService.Entities;

namespace LectioService.Interfaces
{
    public interface IThreadService
    {
        Thread GetThread(Video video, ApplicationUser user, int threadId);

        void AddNewThread(ApplicationUser user, Thread thread, Video video);
    }
}
