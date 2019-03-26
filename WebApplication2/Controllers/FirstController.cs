using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Web.UI;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        public event EventHandler Unload;
        static int level = 0;
        static int quizNum = 0;
        string FileName = @"SavedPlayers.bin";
        static Player player = new Player();
        static List<Player> playersList = new List<Player>();
        Stream SaveFileStream;
        BinaryFormatter serializer;

        public FirstController()
        {
            this.Unload += new System.EventHandler(this.Page_UnLoad);
            string dirPath = Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().Location);
            FileName = dirPath + FileName;
            if (!System.IO.File.Exists(FileName))
            {            
                Stream SaveFileStream = System.IO.File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                SaveFileStream.Close();
            }
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            // code to be executed on user leaves the page    
            int i = 0;
        }

        public ActionResult quizMenu()
        {
            return View();
        }
        public ActionResult startQuiz()
        {
            return RedirectToAction("quiz1");
        }

        public ActionResult quiz1(int result)
        {
            if (result == 0)
            {
                return View();
            }
            else
            {
                player.Score += 10;
                return RedirectToAction("quiz2");

            }
        }

        public ActionResult quiz2()
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
            //save player to file
            Stream SaveFileStream = System.IO.File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, playersList);
            SaveFileStream.Close();

            player.UserName = null;
            return RedirectToAction("Menu");

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Player p)
        {
            if (System.IO.File.Exists(FileName))
            {
                Stream openFileStream = System.IO.File.OpenRead(FileName);
                if (openFileStream.Length != 0)
                {
                    BinaryFormatter deserializer = new BinaryFormatter();
                    playersList = (List<Player>)deserializer.Deserialize(openFileStream);
                }
                openFileStream.Close();
            }

            if (playersList.Count == 0)
            {
                ModelState.AddModelError("username", "username not found or matched");
                return View();
            }
            bool user_exists = false;
            //bool pass_right = false;
            int i = 0; 
            foreach (Player pl in playersList)
            {
                if(p.UserName == pl.UserName && p.Password == pl.Password) {
                    user_exists = true;
                    break;
                }
                i++;
            }

            if (!user_exists)
            {
                ModelState.AddModelError("username", "username not found or matched");
                return View();
            }
            player = playersList[i];
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
        public ActionResult close()
        {
            level = 0;
            return RedirectToAction("Menu");
        }


        [HttpGet]
        public ActionResult Matching1()
        {
            if (level == 0)
            {
                level++;
                return View();
            }

            player.Score = 10;
            return RedirectToAction("Matching2");

        }

        [HttpGet]
        public ActionResult Matching2()
        {
            if (level == 1)
            {
                level++;
                return View();
            }

            player.Score += 20;
            return RedirectToAction("Menu");
        }


        public ActionResult Snake()
        {
            return View();
        }
    }
}
