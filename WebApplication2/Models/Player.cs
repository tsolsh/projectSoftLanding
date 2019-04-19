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
        
        public Player() {
            this.UserName = null;
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
        [Display(Name = "username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Date")]
        public string Date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Score")]
        public int Score { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public List<Player> players {
            get {
                int temp = 0;

                for (int write = 0; write < players.Count; write++)
                {
                    for (int sort = 0; sort < players.Count - 1; sort++)
                    {
                        if (players[sort].Score < players[sort + 1].Score)
                        {
                            temp = players[sort + 1].Score;
                            players[sort + 1].Score = players[sort].Score;
                            players[sort].Score = temp;
                        }
                    }
                }

                return players;
            }
            set { } }

    }
}