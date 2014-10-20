/*
 * Authors:
 * Ian Jones,
 * Jordanne Perry
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LectioServer.Models
{

    public class CommentModel
    {
        /// <summary>
        /// The id of the video the comment belongs to
        /// </summary>
        [Required]
        public int VideoId { get; set; }

        /// <summary>
        /// Comment text
        /// </summary>
        [Required]
        public string CommentText { get; set; }
    }
}