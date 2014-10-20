/*
 * Authors:
 * Jordanne Perry,
 * Will Czifro
 */


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LectioService.Entities
{
    public class Thread
    {
        /// <summary>
        /// Thread Id
        /// </summary>
        public int ThreadId { get; set; }

        //[ForeignKey("Video")]
        //public int VideoId { get; set; }
        //public virtual Video Video { get; set; }

        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }
    }

}
