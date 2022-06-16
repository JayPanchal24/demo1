using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo1.Models
{
    public class GetAPIList
    {
        public List<GetAPI> GetAPI { get; set; }
    }
    public class GetAPI
    {
        public long UserId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public bool Edited { get; set; }
        public bool edited_via_api { get; set; }
    }
}