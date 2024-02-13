using AutoMapper;
using FluentValidation;
using WebShop.Data.Models;
using WebShop.Data.Repositories;
using WebShop.Service.Dtos;

namespace WebShop.Service.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductDto> _productValidator;

        public ProductsService(IProductsRepository productsRepository,
            IMapper mapper,
            IValidator<ProductDto> productValidator)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public async Task<Product?> CreateProductsAsync(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException();
            }

            Product? product;
            try
            {
                var validationResult = await _productValidator.ValidateAsync(productDto);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                product = _mapper.Map<Product>(productDto);
                await _productsRepository.AddAsync(product);
                await _productsRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _productsRepository.GetAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<Product?> UpdateProductAsync(ProductDto productDto)
        {
            var product = await _productsRepository.GetByIdAsync(productDto.Id);

            if (product == null)
            {
                return null;
            }
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;

            await _productsRepository.SaveChangesAsync();

            return product;
        }
    }
}
