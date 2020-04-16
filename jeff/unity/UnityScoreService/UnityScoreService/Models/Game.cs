using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnityScoreService.Models
{
    public class Game
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("Game Name")]
        public string Name { get; set; }
       
        public Game()
        {
            
        }
    }

    
}