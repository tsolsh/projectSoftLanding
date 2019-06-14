﻿using Newtonsoft.Json.Linq;
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
        string FileName = @"SavedPlayers.bin";
        static Player player = new Player();
        static List<Player> playersList = new List<Player>();

        public FirstController()
        {
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


        public ActionResult MultiGames()
        {
            return View();
        }

        public ActionResult localGames()
        {
            return View(player);
        }

        public ActionResult singleGames()
        {
            return View(player);
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
            Player newPlayer = new Player();
            newPlayer.UserName = p.UserName;
            newPlayer.Password = p.Password;
            newPlayer.Email = p.Email;
            newPlayer.FirstName = p.FirstName;
            newPlayer.LastName = p.LastName;
            newPlayer.Date = p.Date;
            playersList.Add(newPlayer);
            player = newPlayer;
            player.players = playersList;
            player.Enter = 1;
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

        public ActionResult Settings()
        {
            if (player.UserName != null)
            {
                return View(player);
            }
            return RedirectToAction("Error");
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
            player.Enter++;
            return RedirectToAction("quiz2");
        }


        [HttpGet]
        public ActionResult quiz2_result(int id)
        {
            player.Score += id * 20;
            player.Enter++;
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
            if (player.Score < 100) {
                return View();
            }
            if (player.Score > 100 && player.Score < 200)
            {
                return RedirectToAction("Matching2");
            }

            return RedirectToAction("Matching3");

        }

        [HttpGet]
        public void matching1_result(int id)
        {
            player.Score += id;
            player.MemoryScore += id;
            player.MemoryLevel = 1;
        }

        public ActionResult Matching2()
        {
            return View();
        }

        public ActionResult middle()
        {
            return RedirectToAction("Matching2");

        }
        [HttpGet]
        public void matching2_result(int id)
        {
            player.Score += id;
            player.MemoryScore += id;
            player.MemoryLevel = 2;
        }

        public ActionResult Matching3()
        {
            return View();
        }

        [HttpGet]
        public void matching3_result(int id)
        {
            player.Score += id;
            player.MemoryScore += id;
            player.MemoryLevel = 3;

        }
        public ActionResult Snake()
        {
            return View(player);
        }
        public ActionResult Snake2()
        {
            return View(player);
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
        [HttpGet]
        public void snake_result(int id, int levelId)
        {
            player.Score += id;
            player.SnakeScore += id;
            player.SnakeLevel = levelId;
        }


        public ActionResult SnakesAndLadders()
        {
            return View(player);
        }

        [HttpGet]
        public ActionResult SnkLds(int id)
        {
            switch (id) {
                case 1:
                    Matching1();
                    break;
                case 2:
                    return RedirectToAction("Matching2");
                case 3:
                    return RedirectToAction("Matching3");

            }
            return RedirectToAction("Matching3");
        }
        [HttpGet]
        public void snakesLadders_result(int id)
        {
            player.Score += id;
            player.snakesLaddersScore += id;
        }

        public ActionResult Read()
        {
            return View(player);
        }

        [HttpGet]
        public void snakesLadders_Qresult(int id)
        {
            player.SNLPos = id;
        }

        [HttpGet]
        public void snakesLadders_QA(int id)
        {
            player.snakesLaddersScore += id;
        }

        public ActionResult TetrisCode()
        {
            if (player.Score < 1000)
            {
                return View();
            }
            else
            {
                return RedirectToAction("TetrisCode2");
            }
        }

        public ActionResult TetrisCode2()
        {
            return View();
        }
        [HttpGet]
        public void tetris_result(int id,int levelId)
        {
            player.Score += id;
            player.TetrisScore += id;
            player.TetrisLevel = levelId;
        }

        public ActionResult Parameters()
        {
            return View();
        }

        public ActionResult Functions()
        {
            return View();
        }

        public ActionResult Langs()
        {
            return View();
        }

        public ActionResult People()
        {
            return View();
        }
        public ActionResult progBasic()
        {
            return View();
        }
        public ActionResult FDE1()
        {
            return View();
        }
        public ActionResult FDE2()
        {
            return View();
        }
        public ActionResult FDE3()
        {
            return View();
        }
        public ActionResult FDE4()
        {
            return View();
        }
        public ActionResult FDE5()
        {
            return View();
        }
        public ActionResult FDE6()
        {
            return View();
        }
        public ActionResult FDE7()
        {
            return View();
        }
        public ActionResult FDE8()
        {
            return View();
        }
        public ActionResult FDE9()
        {
            return View();
        }
        public ActionResult FDE10()
        {
            return View();
        }
        public ActionResult FDE11()
        {
            return View();
        }
        public ActionResult FDE12()
        {
            return View();
        }
        public ActionResult FDE13()
        {
            return View();
        }
        public ActionResult FDE14()
        {
            return View();
        }
        public ActionResult FDE15()
        {
            return View();
        }
        public ActionResult FDE16()
        {
            return View();
        }
        public ActionResult FDE17()
        {
            return View();
        }
        public ActionResult FDE18()
        {
            return View();
        }
        public ActionResult FDE19()
        {
            return View();
        }
        public ActionResult FDE20()
        {
            return View();
        }
        public ActionResult FDE21()
        {
            return View();
        }
        public ActionResult FDE22()
        {
            return View();
        }
        public ActionResult FDE23()
        {
            return View();
        }
        public ActionResult FDE24()
        {
            return View();

        }


        [HttpGet]
        public void exitFDE(int id)
        {

            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/Games/completeTheCode/data.txt"), false))
            {
                for (int i = 0; i < 24; i++)
                {
                    _testData.Write(""); // change WriteLine with Write
                }
                _testData.Close();
            }
            using (StreamWriter file = new StreamWriter(Server.MapPath("~/Games/completeTheCode/data.txt"), true))
            {

                file.WriteLine(id.ToString()); // Write the file.
            }
        }
        [HttpGet]
        public void FDE_result(int id, int levelId)
        {
            player.Score += id;
            player.FillScore += id;
            player.FillLevel = levelId;

        }

        public ActionResult Fill1()
        {
            string level;
            using (StreamReader file = new StreamReader(Server.MapPath("~/Games/completeTheCode/data.txt"), true))
            {
                level = file.ReadLine();
            }
            if (level == "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Fill"+level);

            }
        }
        public ActionResult Fill2()
        {
            return View();
        }
        public ActionResult Fill3()
        {
            return View();
        }
        public ActionResult Fill4()
        {
            return View();
        }
        public ActionResult Fill5()
        {
            return View();
        }
        public ActionResult Fill6()
        {
            return View();
        }
        public ActionResult Fill7()
        {
            return View();
        }
        public ActionResult Fill8()
        {
            return View();
        }
        public ActionResult Fill9()
        {
            string level;
            using (StreamReader file = new StreamReader(Server.MapPath("~/Games/completeTheCode/data.txt"), true))
            {
                level = file.ReadLine();
            }
            if (level == "9")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Fill" + level);

            }
        }
        public ActionResult Fill10()
        {
            return View();
        }
        public ActionResult Fill11()
        {
            return View();
        }
        public ActionResult Fill12()
        {
            return View();
        }
        public ActionResult Fill13()
        {
            return View();
        }
        public ActionResult Fill14()
        {
            return View();
        }
        public ActionResult Fill15()
        {
            return View();
        }
        public ActionResult Fill16()
        {
            return View();
        }
        public ActionResult Fill17()
        {
            string level;
            using (StreamReader file = new StreamReader(Server.MapPath("~/Games/completeTheCode/data.txt"), true))
            {
                level = file.ReadLine();
            }
            if (level == "17")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Fill" + level);

            }
        }
        public ActionResult Fill18()
        {
            return View();
        }
        public ActionResult Fill19()
        {
            return View();
        }
        public ActionResult Fill20()
        {
            return View();
        }
        public ActionResult Fill21()
        {
            return View();
        }
        public ActionResult Fill22()
        {
            return View();
        }
        public ActionResult Fill23()
        {
            return View();
        }
        public ActionResult Fill24()
        {
            return View();
        }
        [HttpGet]
        public void exitFill(int id)
        {
            
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/Games/completeTheCode/data.txt"), false))
            {
                for (int i = 0; i < 24; i++)
                {
                    _testData.Write(""); // change WriteLine with Write
                }
                _testData.Close();
            }
            using (StreamWriter file = new StreamWriter(Server.MapPath("~/Games/completeTheCode/data.txt"), true))
            {

                file.WriteLine(id.ToString()); // Write the file.
            }
        }
        [HttpGet]
        public void fill_result(int id,int levelId)
        {
            player.Score += id;
            player.FillScore += id;
            player.FillLevel = levelId;
            
        }
        [HttpGet]
        public void exitTetris(int id)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/Games/TetrisCode/data.txt"), false))
            {
                for (int i = 0; i < 6; i++)
                {
                    _testData.Write(""); // change WriteLine with Write
                }
                _testData.Close();
            }
            using (StreamWriter file = new StreamWriter(Server.MapPath("~/Games/TetrisCode/data.txt"), true))
            {

                file.WriteLine(id.ToString()); // Write the file.
            }
        }

        [HttpGet]
        public void exitSnake(int id)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/Games/Snake/data.txt"), false))
            {
                for (int i = 0; i < 6; i++)
                {
                    _testData.Write(""); // change WriteLine with Write
                }
                _testData.Close();
            }
            using (StreamWriter file = new StreamWriter(Server.MapPath("~/Games/Snake/data.txt"), true))
            {

                file.WriteLine(id.ToString()); // Write the file.
            }
        }
    }


}
