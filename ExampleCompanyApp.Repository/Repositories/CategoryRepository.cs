using ExampleCompanyApp.Core.Models;
using ExampleCompanyApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ExampleCompanyDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryIdWithProductAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products)
                .Where(x => x.Id == categoryId)
                .SingleOrDefaultAsync();
        }
    }
}
