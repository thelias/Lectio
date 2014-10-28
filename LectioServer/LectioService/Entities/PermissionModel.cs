using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectioService.Entities
{
    /// <summary>
    /// A user will have a general permission, and lecture permission per lecture
    /// </summary>
    public class Permission
    {
        public int PermissionId { get; set; }

        public bool HasCreationPermission { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasUploadPermission { get; set; }

        /// <summary>
        /// Allows user to set permission level of other users, only one user can have this per lecture
        /// </summary>
        public bool HasElevatedPermission { get; set; }

        /// <summary>
        /// Administrative permission, only admin users are granted this
        /// </summary>
        public bool HasAdminPermission { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Lecture")]
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
