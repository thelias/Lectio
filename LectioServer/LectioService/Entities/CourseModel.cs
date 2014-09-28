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
    public class Course
    {
        public int CourseId { get; set; }

        [Display(Name = "Class Course")]
        public string CourseName { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Users { get; set; }

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; } 


    }
}
