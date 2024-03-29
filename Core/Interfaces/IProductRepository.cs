﻿using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>> GetProductsAsync();
}
