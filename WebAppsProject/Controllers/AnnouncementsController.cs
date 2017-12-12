using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppsProject.Data;
using WebAppsProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace WebAppsProject.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AnnouncementsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Announcements
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            /*
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.AddClaimAsync(user, new Claim("Announcements", "CanMakeAnnouncements"));
            */
            IEnumerable<Announcement> ListOfAnnouncements = _context.Announcement.ToList();
            IEnumerable<Comment> ListOfComments = _context.Comment.ToList();

            /*
            foreach(Announcement announcement in ListOfAnnouncements) 
            {
                List<Comment> emptyList = new List<Comment>();
                announcement.Comments = emptyList;
                foreach (Comment comment in ListOfComments) 
                {
                    if (comment.Announcement == announcement) 
                    {
                        announcement.Comments.Add(comment);
                    }
                }
            }
            */

            AnnouncementViewModel model = new AnnouncementViewModel {Announcements = ListOfAnnouncements, Comments = ListOfComments};
            return View(model);
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Announcement announcement = await _context.Announcement
                .SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: Announcements/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Post")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = _userManager.GetUserId(User);
                ApplicationUser currentUser = _context.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                announcement.User = currentUser;

                string currentUserName = _userManager.GetUserName(User);
                announcement.UserName = currentUserName;

                DateTime date = DateTime.Now;
                announcement.Date = date;

                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        // GET: Announcements/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement.SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Post")] Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcement.SingleOrDefaultAsync(m => m.Id == id);
            _context.Announcement.Remove(announcement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcement.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddComment(int id)
        {
            Comment comment = new Comment();
            var announcement = await _context.Announcement
                .SingleOrDefaultAsync(m => m.Id == id);
            comment.Announcement = announcement;
            comment.AId = id;
            return View(comment);
        }

    }
}
