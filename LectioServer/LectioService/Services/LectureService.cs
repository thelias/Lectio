using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public Lecture GetLecture(int lectureId)
        {
            throw new NotImplementedException();
        }

        public void AddNewLecture(Lecture lecture)
        {
            throw new NotImplementedException();
        }

        public void DeleteLecture(Lecture lecture)
        {
            throw new NotImplementedException();
        }

        public void UpdateLecture(Lecture lecture)
        {
            throw new NotImplementedException();
        }
    }
}
