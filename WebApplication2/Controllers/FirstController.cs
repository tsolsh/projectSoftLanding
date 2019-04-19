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
            player.players = playersList;
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

        public ActionResult Menu()
        {
            return View(player);
        }
       

        public ActionResult Leaderboard()
        {
            if (player.UserName != null)
            {
                return View(player);
            }
            return RedirectToAction("Error");
        }


        public ActionResult Play()
        {
            return View();
        }

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
            return RedirectToAction("Menu");
        }

        public ActionResult quizMenu()
        {
            return View();
        }
        public ActionResult startQuiz()
        {
            return RedirectToAction("quiz1");
        }

        public ActionResult quiz1()
        {
            return View();
        }


        [HttpGet]
        public ActionResult quiz1_result(int id)
        {
            player.Score += id * 10;
            return RedirectToAction("quiz2");
        }


        [HttpGet]
        public ActionResult quiz2_result(int id)
        {
            player.Score += id * 20;
            return RedirectToAction("Menu");

        }


        public ActionResult quiz2()
        {
            return View();
        }

        public ActionResult quiz3()
        {
            return View();
        }

        public ActionResult Matching1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult matching1_result(int id)
        {
            player.Score += id;
            return RedirectToAction("Matching2");
        }

        public ActionResult Matching2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult matching2_result(int id)
        {
            player.Score += id;
            return RedirectToAction("Matching3");
        }

        public ActionResult Matching3()
        {
            return View();
        }

        [HttpGet]
        public ActionResult matching3_result(int id)
        {
            player.Score += id;
            return RedirectToAction("Matching4");
        }


        public ActionResult Snake()
        {
            return View();
        }
        public ActionResult Snake2()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Snake_level(int level)
        {
            if (level == 0)
            {
                return RedirectToAction("Snake");
            }
            else
            {
                return RedirectToAction("Snake2");
            }
        }


        public ActionResult SnakesAndLadders()
        {
            return View();
        }

       

    }
}
