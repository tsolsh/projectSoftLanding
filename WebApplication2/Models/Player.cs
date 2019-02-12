using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Player
    {
        public string First;
        public 
        static int count = 0;
        public Player() {
            count++;
        }
        public void copy(Player player) {
            FirstName = player.FirstName;
            Email = player.Email;
            LastName = player.LastName;
            Score = player.Score;
        }
        [Required]
        [Display(Name = "ID")]
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
        [Display(Name = "LastName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Score")]
        public int Score { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}