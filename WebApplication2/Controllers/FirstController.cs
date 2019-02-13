using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        static Player player = new Player();

        public ActionResult MultiGames()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Player p)
        {
            player.UserName = p.UserName;
            player.Password = p.Password;
            return RedirectToAction("Menu");
        }

        public ActionResult Signup()
        {
            return View(player);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Read()
        {
            return View();
        }

        public ActionResult facts()
        {
            return View();
        }

        // GET: First/Create
        public ActionResult Menu()
        {
            return View();
        }
       
        public ActionResult Leaderboard()
        {
            if (player.UserName != null)
            {
                return View();
            }
            return RedirectToAction("Error");
        }
        public ActionResult Play()
        {
            return View();
        }
        // GET: First/Create
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
