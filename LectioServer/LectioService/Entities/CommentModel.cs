using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace LectioService.Entities
{
    public class Comment
    {
        /// <summary>
        /// Comment Id
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Video Id
        /// </summary>
        [ForeignKey("Video")]
        public int VideoId { get; set; }
        [JsonIgnore]
        public virtual Video Video { get; set; }

        /// <summary>
        /// User Id
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
        
        /// <summary>
        /// Comment Text
        /// </summary>
        public string CommentText { get; set; }
    }
}
