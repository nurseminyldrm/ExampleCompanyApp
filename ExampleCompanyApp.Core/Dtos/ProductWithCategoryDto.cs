using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Core.Dtos
{
    public class ProductWithCategoryDto:ProductDto
    {
        public CategoryDto CategoryDto { get; set; }
    }
}
