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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ExampleCompanyDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductWithCategoryAsync()
        {
            return await _context.Products.Include(p=> p.Category).ToListAsync();
        }
    }
}
