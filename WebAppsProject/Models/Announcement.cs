using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppsProject.Models 
{
    public class Announcement 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}
