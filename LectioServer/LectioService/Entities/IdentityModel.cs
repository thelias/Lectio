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
    /// <summary>
    /// This class is used by Microsoft Identity
    /// Only add properties to this class
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /*
         * Properties are mapped to the database by default
         * To prevent a property from being mapped, use the attribute header
         * [NotMapped] above the property.
         * Use [JsonIgnore] to prevent a property from being returned in the http response
         */

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string ProfileUrl { get; set; }

        public virtual IdentityRole Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<Course> Classes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
