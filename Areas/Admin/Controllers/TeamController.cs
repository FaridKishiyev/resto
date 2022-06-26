using FrontToBack5.DAL;
using FrontToBack5.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrontToBack5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Team> team = _context.teams.ToList();
            return View(team);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (team == null)
            {
                return NotFound();
            }

            if (!team.TeamPhoto.ContentType.Contains("image"))
            {
                return NotFound();
            }
            

            string path = _env.WebRootPath; 
            string filename = Guid.NewGuid().ToString() +team.TeamPhoto.FileName;
            string result = Path.Combine(path,"assets","img", filename);

            using (FileStream stream = new FileStream(result, FileMode.Create))
            {
                team.TeamPhoto.CopyTo(stream);
            }

            Team newteam = new Team();
            newteam.PersonName = team.PersonName;
            newteam.Position = team.Position;
            newteam.İmageUrl = filename;

            _context.teams.Add(newteam);
            _context.SaveChanges();

            return RedirectToAction("Index","Team");
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = _context.teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }


            


            return View(team);
        }

        [HttpPost]
        public IActionResult Update(int? id,Team teams)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = _context.teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }
            string filename;
            if (teams.TeamPhoto != null)
            {
                string path = _env.WebRootPath;
                filename = Guid.NewGuid().ToString() + teams.TeamPhoto.FileName;
                string result = Path.Combine(path, "assets", "img", filename);
                using (FileStream stream = new FileStream(result, FileMode.Create))
                {
                    teams.TeamPhoto.CopyTo(stream);
                }
            }
            else
            {
                 filename = team.İmageUrl;
            }




            team.PersonName = teams.PersonName;
            team.Position = teams.Position;
            team.İmageUrl = filename;

            _context.SaveChanges();

            return RedirectToAction("Index","Team");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = _context.teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            _context.teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction("Index", "Team");
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team team = _context.teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
    }
}
