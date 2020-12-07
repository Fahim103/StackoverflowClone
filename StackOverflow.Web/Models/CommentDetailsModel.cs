using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class CommentDetailsModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AnsweredBy{ get; set; }
        public bool IsAccepted { get; set; }
        public int Points { get; set; }

    }
}