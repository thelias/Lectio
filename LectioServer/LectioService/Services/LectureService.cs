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
    public class LectureService : ILectureService
    {
        private readonly LectioContext _context;

        public LectureService(LectioContext context)
        {
            _context = context;
        }
        public List<Lecture> GetLectures(ApplicationUser user, int pg, int num)
        {
            var list = user.Lectures.Skip(pg*num).Take(num).ToList();
            return list;
        }

        public Lecture GetLecture(int lectureId)
        {
            var lect = _context.Lectures.SingleOrDefault(x => x.LectureId == lectureId);

            return lect;
        }

        public void AddNewLecture(ApplicationUser user, Lecture lecture)
        {
            if (user.Lectures == null || !user.Lectures.Any())
            {
                user.Lectures = new Collection<Lecture>();
            }

            user.Lectures.Add(lecture);
            _context.SaveChanges();
        }

        public void DeleteLecture(ApplicationUser user, Lecture lecture)
        {

            var lect = user.Lectures.SingleOrDefault(x => x.LectureId == lecture.LectureId);

            if (lect == null) return;
            _context.Lectures.Remove(lecture);
            _context.SaveChanges();
        }

        public void UpdateLecture(ApplicationUser user, Lecture lecture)
        {
            var lect = user.Lectures.SingleOrDefault(x => x.LectureId == lecture.LectureId);

            if (lect == null) return;
            lect = lecture;
            _context.SaveChanges();
        }
    }
}
