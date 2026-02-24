using ExampleCompanyApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Core.Services
{
    public interface ICategoryService
    {
        Task<CustomReponseDto<List<CategoryWithProductDto>>> 
            GetSingleCategoryWithProductAsync(int categoryId);
    }
}
