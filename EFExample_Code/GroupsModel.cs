using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WeKeepsService.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Keep> Keeps { get; set; }
        [JsonIgnore]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Invite> Invites { get; set; }
    }

    public class GroupMember
    {
        public int GroupMemberId { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }

    public class Invite
    {
        public int InviteId { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public string RecipientUserId { get; set; }
        public virtual ApplicationUser RecipientUser { get; set; }
        [Required]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        [Required]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
