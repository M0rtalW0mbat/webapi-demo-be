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
using model;
using api.Models;

namespace api.Controllers
{
    public class ArticlesController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/Articles
        public IQueryable<ArticleDTO> GetArticles()
        {
            var query =
                from a in db.Articles
                select new ArticleDTO()
                {
                    Id = a.Id,
                    Body = a.Body,
                    Title = a.Title,
                    CreationDate = a.CreationDate
                };
            return query;
        }

        // GET: api/Articles/5
        [ResponseType(typeof(ArticleDTO))]
        public IHttpActionResult GetArticle(int id)
        {
            var article =
                (from a in db.Articles
                where a.Id == id
                select new ArticleDTO()
                {
                    Id = a.Id,
                    Body = a.Body,
                    Title = a.Title,
                    CreationDate = a.CreationDate
                }).SingleOrDefault();
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, ArticleDTO article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            var updateArticle = db.Articles.Find(id);
            updateArticle.Title = article.Title;
            updateArticle.Body = article.Body;
            //db.Entry(updateArticle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        [ResponseType(typeof(ArticleDTO))]
        public IHttpActionResult PostArticle(ArticleDTO article)
        {
            var newArticle = new Article()
            {
                Title = article.Title,
                Body = article.Body
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articles.Add(newArticle);
            db.SaveChanges();

            article.Id = newArticle.Id;
            article.CreationDate = newArticle.CreationDate;

            return CreatedAtRoute("DefaultApi", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(ArticleDTO))]
        public IHttpActionResult DeleteArticle(int id)
        {
            var article = db.Articles.Find(id);
            var articleDTO =
                (from a in db.Articles
                 where a.Id == id
                 select new ArticleDTO()
                 {
                     Id = a.Id,
                     Body = a.Body,
                     Title = a.Title,
                     CreationDate = a.CreationDate
                 }).SingleOrDefault();
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return Ok(articleDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.Id == id) > 0;
        }
    }
}