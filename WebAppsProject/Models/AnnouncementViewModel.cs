using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppsProject.Models
{
    public class AnnouncementViewModel
    {
        public IEnumerable<Announcement> Announcements;
        public IEnumerable<Comment> Comments;
    }
}
