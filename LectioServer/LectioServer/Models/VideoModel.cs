﻿using System;
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
        /// <summary>
        /// the lecture id the video belongs to
        /// </summary>
        [Required]
        public int LectureId { get; set; }

    }
}