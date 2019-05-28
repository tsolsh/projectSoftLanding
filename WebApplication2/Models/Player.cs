using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    [Serializable()]
    public class Player
    {
        private List<Player> pla;
        public Player() {
            this.UserName = "adi";
            this.pla = new List<Player>();
            this.Enter = 0;
            this.Score = 0;
            this.MemoryLevel = 0;
            this.TetrisLevel = 0;
            this.SnakeNLaddersLevel = 0;
            this.SnakeLevel = 0;
            this.SimonLevel = 0;
            this.RPSLevel = 0;
        }
        public void copy(Player player) {
            FirstName = player.FirstName;
            Email = player.Email;
            LastName = player.LastName;
            Score = player.Score;
        }
        [Required]
        [Display(Name = "password")]
        public int Password { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Date")]
        public string Date { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public List<Player> players
        {
            get
            {
                int temp = 0;
                for (int write = 0; write < pla.Count; write++)
                {
                    for (int sort = 0; sort < pla.Count - 1; sort++)
                    {
                        if (pla[sort].Score < pla[sort + 1].Score)
                        {
                            temp = pla[sort + 1].Score;
                            pla[sort + 1].Score = pla[sort].Score;
                            pla[sort].Score = temp;
                        }
                    }
                }

                return pla;
            }
            set { pla = value; }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int Enter { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Advance")]
        public int Advance { get {
                int adv = 0;
                int totalLevels = MemoryLevel + TetrisLevel + SnakeLevel + SnakeNLaddersLevel + SimonLevel +RPSLevel;
                adv = totalLevels *10/100;
                return adv;
            } set { } }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "GamePlayed")]
        public int GamePlayed { get {
                int games = 0;
                if (this.MemoryLevel != 0)
                {
                    games++;
                }
                if (this.TetrisLevel != 0)
                {
                    games++;
                }
                if (this.SnakeNLaddersLevel != 0){
                    games++;
                }
                if (this.SnakeLevel != 0){
                    games++;
                }

                if (this.SimonLevel != 0)
                {
                    games++;
                }
                if (this.RPSLevel != 0)
                {
                    games++;
                }
                return games;

            }

            set { } }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int Score { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int SnakeScore { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int TetrisScore { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int MemoryScore { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int snakesLaddersScore { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int SimonScore { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int RPSScore { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Level")]
        public int TetrisLevel { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Level")]
        public int MemoryLevel { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Level")]
        public int SnakeLevel { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Level")]
        public int SnakeNLaddersLevel { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Level")]
        public int SimonLevel { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Level")]
        public int RPSLevel { get; set; }

    }
}