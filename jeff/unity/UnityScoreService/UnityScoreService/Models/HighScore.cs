using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnityScoreService.Models
{
    public class HighScore
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("Player Name")]
        public string PlayerName { get; set; }
        
        public int Score { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }



}