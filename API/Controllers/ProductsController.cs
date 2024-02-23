using API.Dto;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductBrand> _brandReoisitory;
    private readonly IGenericRepository<ProductType> _typeReoisitory;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> brandReoisitory,
        IGenericRepository<ProductType> typeReoisitory,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _brandReoisitory = brandReoisitory;
        _typeReoisitory = typeReoisitory;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productPrams)
    {
        var spec = new ProductsWithTypesAndBrandsSpec(productPrams);
        var countSpec = new ProductsWithFilterForCountSpecification(productPrams);
        var totalItems = await _productRepository.CountAsync(countSpec);
        var products = await _productRepository.ListAsync(spec);
        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        return Ok(new Pagination<ProductToReturnDto>(productPrams.PageIndex, productPrams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpec(id);
        var product = await _productRepository.GetEntityWithSpec(spec);
        if (product == null) { return NotFound(new ApiResponse(404)); }
        return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    [HttpGet("brands")]
    public async Task<IReadOnlyList<ProductBrand>> GetBrands()
    {
        return await _brandReoisitory.ListAllAsync();
    }

    [HttpGet("types")]
    public async Task<IReadOnlyList<ProductType>> GetTypes()
    {
        return await _typeReoisitory.ListAllAsync();
    }
}
