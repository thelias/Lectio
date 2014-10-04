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
    public class VideoService : IVideoService
    {
        private readonly LectioContext _context;
        public VideoService(LectioContext context)
        {
            _context = context;
        }
        public Video GetVideo(ApplicationUser user, int videoId)
        {
            var video = user.Videos.SingleOrDefault(x => x.VideoId == videoId);
            return video;
        }

        public List<Video> GetVideos(ApplicationUser user, Lecture lecture, int pg, int num)
        {
            var videos = lecture.Videos.Skip(pg*num).Take(num).ToList();
            return videos;
        }

        public void AddNewVideo(ApplicationUser user, Lecture lecture, Video video)
        {
            var lect = user.Lectures.SingleOrDefault(x => x.LectureId == lecture.LectureId);
            if (lect == null)
                throw new Exception("Lecture not found");
            if (lect.Videos == null || !lect.Videos.Any())
                lect.Videos = new Collection<Video>();
            lect.Videos.Add(video);
            _context.SaveChanges();
        }

        public void DeleteVideo(ApplicationUser user, Lecture lecture, Video video)
        {
            var lect = user.Lectures.SingleOrDefault(x => x.LectureId == lecture.LectureId);
            if (lect == null)
                throw new Exception("Lecture not found");
            if (lect.Videos == null || !lect.Videos.Any())
                throw new Exception("No videos to delete");
            var vid = lect.Videos.SingleOrDefault(v => v.VideoId == video.VideoId);
            if (vid == null)
                throw new Exception("Video does not exist");
            lect.Videos.Remove(vid);
            _context.SaveChanges();
        }

        public void UpdateVideo(ApplicationUser user, Video video)
        {
            var vid = user.Videos.SingleOrDefault(x => x.VideoId == video.VideoId);
            if (vid == null)
                throw new Exception("Access denied");
            vid = video;
            _context.SaveChanges();
        }
    }
}
