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
            this.UserName = null;
            this.pla = new List<Player>();
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
    }
}