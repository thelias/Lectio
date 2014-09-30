using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectioService.Entities;

namespace LectioService.Interfaces
{
    public interface ILectureService
    {
        List<Lecture> GetLectures(ApplicationUser user, int pg, int num);

        Lecture GetLecture(int lectureId);

        void AddNewLecture(ApplicationUser user, Lecture lecture);

        void DeleteLecture(ApplicationUser user, Lecture lecture);

        void UpdateLecture(ApplicationUser user, Lecture lecture);

    }
}
