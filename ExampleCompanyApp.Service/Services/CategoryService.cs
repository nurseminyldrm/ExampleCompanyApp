using AutoMapper;
using ExampleCompanyApp.Core.Dtos;
using ExampleCompanyApp.Core.Models;
using ExampleCompanyApp.Core.Repositories;
using ExampleCompanyApp.Core.Services;
using ExampleCompanyApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> genericRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomReponseDto<List<CategoryWithProductDto>>> GetSingleCategoryWithProductAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryIdWithProductAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryWithProductDto>(category);
            var list = new List<CategoryWithProductDto>() { categoryDto };
            return CustomReponseDto<List<CategoryWithProductDto>>.Success(200, list);
        }
    }
}
