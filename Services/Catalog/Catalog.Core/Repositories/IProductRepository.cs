﻿using Catalog.Core.Entities;
using Catalog.Core.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams);
        Task<Product> GetProductById(string Id);
        Task<bool> DeleteProductId(string Id);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<IEnumerable<Product>> GetProductsByName(string Name);
        Task<IEnumerable<Product>> GetProductsByBrand(string BrandName);
    }
}
