/*
 * Authors:
 * Will Czifro,
 * Jordanne Perry,
 */

using System.Collections.Generic;
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
