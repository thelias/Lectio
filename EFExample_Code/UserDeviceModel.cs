using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeKeepsService.Entities
{
    public class UserDevice
    {
        public int UserDeviceId { get; set; }
        public string DeviceToken { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
