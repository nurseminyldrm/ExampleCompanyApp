using AutoMapper;
using ExampleCompanyApp.Api.Filters;
using ExampleCompanyApp.Core.Dtos;
using ExampleCompanyApp.Core.Models;
using ExampleCompanyApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExampleCompanyApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productService.GetAllAsync();
            var productDto = _mapper.Map<List<ProductDto>>(product).ToList();
            return CreateActionResult(CustomReponseDto<List<ProductDto>>.Success(200,productDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomReponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
             var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            var productDtoResult = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomReponseDto<ProductDto>.Success(201, productDtoResult));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductUpdateDto productUpdate)
        {
            if (id!=productUpdate.Id)
            {
                return BadRequest("ids not match!");
            }
            await _productService.UpdateAsync(_mapper.Map<Product>(productUpdate));
            return CreateActionResult(CustomReponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return CreateActionResult(CustomReponseDto<NoContentDto>.Success(204));
        }
    }
}
