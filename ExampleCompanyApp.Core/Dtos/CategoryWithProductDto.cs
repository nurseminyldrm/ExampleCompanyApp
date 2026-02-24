using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Core.Dtos
{
    public class CategoryWithProductDto:CategoryDto
    {
        public List<ProductDto> ProductDto { get; set; }
    }
}
