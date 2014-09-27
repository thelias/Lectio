using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeKeepsService.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusValue { get; set; }
        [JsonIgnore]
        public ICollection<Invite> Invites { get; set; }
        [JsonIgnore]
        public ICollection<FriendRequest> FriendRequests { get; set; }
    }
}
