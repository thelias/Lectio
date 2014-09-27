using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WeKeepsService.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        [Display(Name = "Message")]
        public string UserMessage { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? TimeStamp { get; set; }
        [Required]
        public int KeepId { get; set; }
        [JsonIgnore]
        public virtual Keep Keep { get; set; }
        [Required]
        public string UserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
    }

    public class UserReadMessage
    {
        public int UserReadMessageId { get; set; }
        public int KeepId { get; set; }
        public virtual Keep Keep { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
