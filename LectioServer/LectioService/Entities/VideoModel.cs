/*
 * Authors:
 * Ian Jones,
 * Will Czifro
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace LectioService.Entities
{
    public class Video
    {
        /// <summary>
        /// Video Id
        /// </summary>
        public int VideoId { get; set; }
        [JsonIgnore]
        public string VideoName { get; set; }

        /// <summary>
        /// Url link to video
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// Url link to thumbnail
        /// </summary>
        public string ThumbnailUrl { get; set; }

    
        /// <summary>
        /// Timestamp when video was uploaded
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Lecture Id
        /// </summary>
        [ForeignKey("Lecture")]
        public int LectureId { get; set; }
        [JsonIgnore]
        public virtual Lecture Lecture { get; set; }

        /// <summary>
        /// User who uploaded it
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
        /// <summary>
        /// Thread Id
        /// </summary>
        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
        [JsonIgnore]
        public virtual Thread Thread { get; set; }
       
    }
}
