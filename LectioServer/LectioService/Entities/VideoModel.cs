/*
 * Authors:
 * Ian Jones,
 * Will Czifro
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectioService.Entities
{
    public class Video
    {
        public int VideoId { get; set; }

        public string VideoName { get; set; }

        public string VideoUrl { get; set; }

        public string ThumbnailUrl { get; set; }

    

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeStamp { get; set; }

        [ForeignKey("Lecture")]
        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Thread")]
        public int ThreadId { get; set; }

        public virtual Thread Thread { get; set; }
       
    }
}
