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
        static List<Player> playersList = new List<Player>();
        public ActionResult quizMenu()
        {
            return View();
        }
        public ActionResult startQuiz()
        {
            return View();
        }

        public ActionResult MultiGames()
        {
            return View();
        }

        public ActionResult localGames()
        {
            return View();
        }

        public ActionResult singleGames()
        {
            return View();
        }

        public ActionResult LogOut()
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
            if(playersList == null)
            {
                ModelState.AddModelError("username", "username not found or matched");
                return View();
            }
            bool user_exists = false;
            bool pass_right = false;

            foreach (Player pl in playersList)
            {
                if(p.UserName == pl.UserName) {
                    user_exists = true;
                }
            }
            foreach (Player pl in playersList)
            {
                if (p.Password == pl.Password)
                {
                    pass_right = true;
                }
            }
            if (!user_exists || !pass_right)
            {
                ModelState.AddModelError("username", "username not found or matched");
                return View();
            }
            return RedirectToAction("Menu");
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Player p)
        {
            player.UserName = p.UserName;
            player.Password = p.Password;
            player.Email = p.Email;
            player.FirstName = p.FirstName;
            player.LastName = p.LastName;
            player.Date = p.Date;
            playersList.Add(player);
            return RedirectToAction("Menu");
        }
        public ActionResult Contact()
        {
            try {
                return View(player);
            }
            catch
            {
                return RedirectToAction("Menu");
            }
        }

        public ActionResult Read()
        {
            return View();
        }

        public ActionResult facts(Player p)
        {
            return View(p);
        }

        // GET: First/Create
        public ActionResult Menu()
        {
            return View(player);
        }
       

        public ActionResult Leaderboard(Player p)
        {
            if (p.UserName != null)
            {
                return View(p);
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

        public ActionResult Error(Player p)
        {
            return View(p);
        }

        public ActionResult Settings(Player p)
        {
            return View(p);
        }

    }
}
