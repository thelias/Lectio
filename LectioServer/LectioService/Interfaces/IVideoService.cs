using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectioService.Entities;

namespace LectioService.Interfaces
{
    public interface IVideoService
    {
        Video GetVideo(ApplicationUser user, int videoId);

        List<Video> GetVideos(ApplicationUser user, Lecture lecture, int pg, int num);

        void AddNewVideo(ApplicationUser user, Lecture lecture, Video video);

        void DeleteVideo(ApplicationUser user, Lecture lecture, Video video);

        void UpdateVideo(ApplicationUser user, Video video);
    }
}
