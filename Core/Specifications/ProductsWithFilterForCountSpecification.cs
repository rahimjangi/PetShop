﻿using Core.Entities;

namespace Core.Specifications;

public class ProductsWithFilterForCountSpecification : BaseSpecification<Product>
{
    public ProductsWithFilterForCountSpecification(ProductSpecParams specParams) : base(

            x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId) &&
            (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)

            )
    {
    }
}
