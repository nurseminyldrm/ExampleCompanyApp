using ExampleCompanyApp.Core.Dtos;
using ExampleCompanyApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task<CustomReponseDto<List<ProductWithCategoryDto>>>GetProductWithCategoryAsync();
    }
}
