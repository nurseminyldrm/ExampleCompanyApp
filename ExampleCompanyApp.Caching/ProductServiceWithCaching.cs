using AutoMapper;
using ExampleCompanyApp.Core.Dtos;
using ExampleCompanyApp.Core.Models;
using ExampleCompanyApp.Core.Repositories;
using ExampleCompanyApp.Core.Services;
using ExampleCompanyApp.Core.UnitOfWorks;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        
        //cache işlemleri buradan yapılacak

        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, 
            IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            if(!_memoryCache.TryGetValue(CacheProductKey,out _))
            {
                var products = _productRepository.GetProductWithCategoryAsync().Result.ToList();
                _memoryCache.Set(CacheProductKey, products);
            }
        }





        public Task<CustomReponseDto<List<ProductWithCategoryDto>>> GetProductWithCategoryAsync()
        {
            var product = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return Task.FromResult(CustomReponseDto<List<ProductWithCategoryDto>>.Success(200, productDto));
        }
        public async Task CacheAllProductAsync()
        {
            _memoryCache.Set(CacheProductKey, await _productRepository.GetProductWithCategoryAsync());

        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                throw new DirectoryNotFoundException($"{nameof(Product)} - ({id}) not found");
            }
            return Task.FromResult(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).AsQueryable()
                .Where(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return await _productRepository.AnyAsync(expression);
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _productRepository.AddRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
            return entities;
        }

        public async Task UpdateAsync(Product entity)
        {
            // SQL'in kızdığı o tarih alanlarını elinle doldur, konu kapansın
            entity.UpdatedDate = DateTime.Now;

            // Eğer CreatedDate de null gelip hata verirse, onu da bir şekilde doldurman gerekebilir
            // entity.CreatedDate = DateTime.Now; 

            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        /* public async Task UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        } */

        public async Task RemoveAsync(Product entity)
        {
            _productRepository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _productRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }
        
    }
}
