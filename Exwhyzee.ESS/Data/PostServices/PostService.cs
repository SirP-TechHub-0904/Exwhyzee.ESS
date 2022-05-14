using Exwhyzee.ESS.Models;
using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Exwhyzee.ESS.Data.PostServices
{
    public class PostService : IPostService
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public async Task<string> Create(Post model, HttpPostedFileBase upload)
        {
            try
            {

                if (upload != null && upload.ContentLength > 0)
                {


                    string date1 = DateTime.UtcNow.AddHours(1).ToString("ssfff");
                    string name = date1 + "-" + upload.FileName;
                    string fileName = Path.GetFileName(name);
                    model.PostImage = fileName;
                    fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), fileName);
                    upload.SaveAs(fileName);
                    db.Posts.Add(model);
                    await db.SaveChangesAsync();
                    
                }
                return "OK";
            }
            catch (Exception c)
            {
return c.ToString();
            }


            
        }

        public async Task Delete(int? id)
        {
            var item = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (File.Exists(HttpContext.Current.Server.MapPath("~/Uploads/" + item.PostImage)))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/" + item.PostImage));
            }
            if (item != null)
            {
                db.Posts.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Post> Details(int? id)
        {
            var item = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);


            return item;
        }

        public async Task<string> Edit(Post models, HttpPostedFileBase upload)
        {
            try
            {


                if (File.Exists(HttpContext.Current.Server.MapPath("~/Uploads/" + models.PostImage)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/" + models.PostImage));
                }
            }catch(Exception c) { }
            try
            {

                if (upload != null && upload.ContentLength > 0)
                {


                    string date1 = DateTime.UtcNow.AddHours(1).ToString("ssfff");
                    string name = date1 + "-" + upload.FileName;
                    string fileName = Path.GetFileName(name);
                    models.PostImage = fileName;
                    fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), fileName);
                    upload.SaveAs(fileName);
                    db.Entry(models).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                }
                return "OK";
            }
            catch (Exception c)
            {

            }


            return "404";
        }

        public async Task<Post> Get(int? id)
        {
            var item = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<Post>> List(string searchString, string currentFilter, int? page)
        {
            var items = db.Posts.Where(x => x.Title != "");
            if (!String.IsNullOrEmpty(searchString))
            {

                items = items.Where(s => s.Content.ToUpper().Contains(searchString.ToUpper()) || s.Title.ToUpper().Contains(searchString.ToUpper()));

            }
            return await items.ToListAsync();
        }

    }
}