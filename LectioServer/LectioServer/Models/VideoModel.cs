using System;
/*
 * Author:
 * Ian Jones
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LectioServer.Models
{
    public class VideoModel
    {
        [Required]
        public int LectureId { get; set; }

    }
}