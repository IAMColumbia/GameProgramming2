using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UnityScoreService.Models
{
    public class DBContext : DbContext
    {

        public DBContext() : base("DBContext")
        {
            //Database.SetInitializer<DBContext>(new DropCreateDatabaseAlways<DBContext>());
            //Database.SetInitializer<DBContext>(new CreateDatabaseIfNotExists<DBContext>());
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<HighScore> HighScores { get; set; }
    }
}