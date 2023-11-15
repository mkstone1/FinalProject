using BackendDataAccess.Models.Categories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDataAccess.Models.Categories.Infrastructure
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();

        Task UpsertCategory(Category category);
    }
}
