using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace WeKeepsService.Entities
{
    #region Unmapped Classes
    public class InboxItem
    {
        public string UserName { get; set; }
        public Keep Keep { get; set; }
        public Message Message { get; set; }
        public bool IsNew { get; set; }
    }

    public class FacebookPost
    {
        public string Title { get; set; }
        public string UserCaption { get; set; }
        public string MetaCaption { get; set; }
        public string Link { get; set; }
        public string Picture { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Timestamp { get; set; }

    }

    #endregion
}
