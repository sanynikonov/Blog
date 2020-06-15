using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogListItemModel>> GetAll();
        Task<BlogModel> GetById(int id);
        Task<IEnumerable<BlogActivityInfoModel>> GetByBiggestActivityInPeriod(DateTime oldest, DateTime latest);
        Task Create(BlogModel model, string userId);
    }
}
