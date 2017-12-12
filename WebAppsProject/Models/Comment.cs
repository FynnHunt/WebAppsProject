using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppsProject.Models
{
    public class Comment 
    {
        public int Id { get; set; }
        public string Post { get; set; }
        public DateTime Date { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserName { get; set; }
        public virtual Announcement Announcement { get; set; }
        public int AId { get; set; }
    }
}
