using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UnityScoreService.Models;

namespace UnityScoreService.Controllers
{
    public class HighScoresController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/HighScores
        public IQueryable<HighScore> GetHighScores()
        {
            return db.HighScores;
        }

        // GET: api/HighScores
        public IQueryable<HighScore> GetHighScores(int GameId)
        {
            return db.HighScores.Where(hs=>hs.GameId == GameId);
        }

        // GET: api/HighScores/5
        [ResponseType(typeof(HighScore))]
        public IHttpActionResult GetHighScore(int id)
        {
            HighScore highScore = db.HighScores.Find(id);
            if (highScore == null)
            {
                return NotFound();
            }

            return Ok(highScore);
        }

        

        // PUT: api/HighScores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHighScore(int id, HighScore highScore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != highScore.Id)
            {
                return BadRequest();
            }

            db.Entry(highScore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HighScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HighScores
        [ResponseType(typeof(HighScore))]
        public IHttpActionResult PostHighScore(HighScore highScore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HighScores.Add(highScore);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = highScore.Id }, highScore);
        }

        // DELETE: api/HighScores/5
        [ResponseType(typeof(HighScore))]
        public IHttpActionResult DeleteHighScore(int id)
        {
            HighScore highScore = db.HighScores.Find(id);
            if (highScore == null)
            {
                return NotFound();
            }

            db.HighScores.Remove(highScore);
            db.SaveChanges();

            return Ok(highScore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HighScoreExists(int id)
        {
            return db.HighScores.Count(e => e.Id == id) > 0;
        }
    }
}