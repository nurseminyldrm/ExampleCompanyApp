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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork,
            IProductRepository productRepository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomReponseDto<List<ProductWithCategoryDto>>> GetProductWithCategoryAsync()
        {
            var product = await _productRepository.GetProductWithCategoryAsync();
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomReponseDto<List<ProductWithCategoryDto>>.Success(200, productDto);
        }

        Task<CustomReponseDto<List<ProductWithCategoryDto>>> IProductService.GetProductWithCategoryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
