/*
 * Author:
 * Ian Jones
 */

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
    public class Lecture
    {
        /// <summary>
        /// Lecture Id
        /// </summary>
        public int LectureId { get; set; }

        /// <summary>
        /// Lecture Name
        /// </summary>
        [Display(Name = "Lecture Name")]
        public string LectureName { get; set; }

        ///// <summary>
        ///// Lecture Creator
        ///// </summary>
        //[ForeignKey("Creator")]
        //public string CreatorId { get; set; }
        //public ApplicationUser Creator { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Users { get; set; }

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; } 


    }
}
