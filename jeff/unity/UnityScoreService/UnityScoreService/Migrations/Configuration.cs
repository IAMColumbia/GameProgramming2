namespace UnityScoreService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UnityScoreService.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UnityScoreService.Models.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.Games.AddOrUpdate(g => g.Id, 
                new Models.Game() { Id = 1, Name = "Deafult Game" });

            context.HighScores.AddOrUpdate(h => h.Id,
                new Models.HighScore() { PlayerName = "Jeff", GameId = 1, Score = 100 },
                new Models.HighScore() { PlayerName = "Jeff", GameId = 1, Score = 200 }
                );

        }
    }
}
