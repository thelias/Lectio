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

namespace WeKeepsService.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "User Photo")]
        public string PhotoUrl { get; set; }
        [Display(Name = "Bio")]
        public string Bio { get; set; }
        #region NotMapped
        [NotMapped]
        
        public List<Keep> MutualKeeps { get; set; }
        #endregion
        #region JsonIgnore
        [JsonIgnore]
        public virtual ICollection<Keep> Keeps { get; set; }
        [JsonIgnore]
        public virtual ICollection<Friend> Friends { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserReadMessage> UserReadMessages { get; set; }
        [JsonIgnore]
        public virtual ICollection<Invite> Invites { get; set; }
        [JsonIgnore]
        public virtual ICollection<FriendRequest> FriendRequests { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserDevice> UserDevices { get; set; }
        [JsonIgnore]
        public virtual ICollection<QueryResult> QueryResults { get; set; }
        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
