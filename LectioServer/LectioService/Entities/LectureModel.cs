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
        public int LectureId { get; set; }

        [Display(Name = "Lecture Name")]
        public string LectureName { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Users { get; set; }

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; } 


    }
}
