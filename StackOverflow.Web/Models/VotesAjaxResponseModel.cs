using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Web.Models
{
    public class VotesAjaxResponseModel
    {
        public int Id { get; set; }
        public long Points { get; set; }
        public string Status { get; set; }

        public VotesAjaxResponseModel()
        {
            Status = "NOT_AUTHENTICATED";
        }
    }
}