using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace WeKeepsService.Entities
{
    public class Friend
    {
        public int FriendId { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }  // User
        [ForeignKey("FriendUser")]
        public string FriendUserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser FriendUser { get; set; }  // Friend
    }

    public class FriendRequest
    {
        public int FriendRequestId { get; set; }
        public string SendUserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser SendUser { get; set; }
        public string SendUserFirstName { get; set; }
        public string SendUserLastName { get; set; }
        public string SendUserPhotoUrl { get; set; }
        public string RecipientUserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser RecipientUser { get; set; }
        [Required]
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
