using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LectioServer.Models
{
    public class ThreadModel
    {
        [Required]
        public int VideoId { get; set; }

    }

    public class CommentModel
    {
        [Required]
        public int VideoId { get; set; }

        [Required]
        public string CommentText { get; set; }
    }
}