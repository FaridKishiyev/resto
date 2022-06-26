using FrontToBack5.DAL;
using FrontToBack5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FrontToBack5.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
             _context = context;
        }
        public IActionResult Index()
        {
            List<Team> team = _context.teams.ToList();

            return View(team);
        }
    }
}
