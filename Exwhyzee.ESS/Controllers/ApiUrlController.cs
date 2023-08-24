using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Exwhyzee.ESS.Helper;
using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using Exwhyzee.ESS.Models.Entities.Dto;

namespace Exwhyzee.ESS.Controllers
{
    public class ApiUrlController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Posts
        public IQueryable<PostDto> GetPosts(int size)
        {
            try
            {
                var sresult = db.Posts.ToList();
                //http://iskools.com.ng/Uploads/
                var query = sresult.Select(c => new PostDto()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.Content,
                    PreviewContent = c.PreviewContent,
                    DatePosted = c.DatePosted,
                    Status = c.Status,
                    WhoCanSeePost = c.WhoCanSeePost,
                    PostedBy = c.PostedBy,
                    SortOrder = c.SortOrder,
                    PostImage = "http://iskools.com.ng/Uploads/" + c.PostImage
                }).ToList();

                var output = IskoolHelper.Shuffle(query, size);
                IQueryable<PostDto> queryable = output.AsQueryable();
                return queryable;

            }
            catch(Exception c)
            {

            }
            return null;
           
        }
       
        // GET: api/Posts/5
        [ResponseType(typeof(Post))]
        [HttpGet]
        public async Task<IHttpActionResult> GetPost(int id)
        {
            Post c = await db.Posts.FindAsync(id);

            var post = new PostDto()
            {
                Id = c.Id,
                Title = c.Title,
                Content = c.Content,
                PreviewContent = c.PreviewContent,
                DatePosted = c.DatePosted,
                Status = c.Status,
                WhoCanSeePost = c.WhoCanSeePost,
                PostedBy = c.PostedBy,
                SortOrder = c.SortOrder,
                PostImage = "http://iskools.com.ng/Uploads/" + c.PostImage
            };

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> DeletePost(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            await db.SaveChangesAsync();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }
    }
}