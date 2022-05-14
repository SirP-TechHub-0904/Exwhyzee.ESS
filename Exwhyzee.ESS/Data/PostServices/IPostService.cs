using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Exwhyzee.ESS.Data.PostServices
{
   public interface IPostService
    {
        Task<string> Create(Post model, HttpPostedFileBase upload);
        Task<Post> Get(int? id);
        Task<string> Edit(Post models, HttpPostedFileBase upload);
        Task Delete(int? id);
        Task<List<Post>> List(string searchString, string currentFilter, int? page);



    }
}
