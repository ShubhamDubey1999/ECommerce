using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, ITypesRepository, IBrandRepository
    {
        public ICatalogContext _context { get; }
        public ProductRepository(ICatalogContext catalogContext)
        {
            _context = catalogContext;
        }


        async Task<Product> IProductRepository.CreateProduct(Product product)
        {
            await _context
                .Products
                .InsertOneAsync(product);
            return product;
        }

        async Task<bool> IProductRepository.DeleteProductId(string Id)
        {
            var result = await _context.Products.DeleteOneAsync(x => x.Id == Id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        async Task<IEnumerable<ProductBrand>> IBrandRepository.GetAllBrands()
        {
            return await _context
                .Brands
                .Find(b => true)
                .ToListAsync();
        }

        async Task<IEnumerable<ProductType>> ITypesRepository.GetAllTypes()
        {
            return await _context
                .Types
                .Find(b => true)
                .ToListAsync();
        }

        async Task<Product> IProductRepository.GetProductById(string Id)
        {
            return await _context
                .Products
                .Find(p => p.Id == Id)
                .FirstOrDefaultAsync();
        }

        async Task<Pagination<Product>> IProductRepository.GetProducts(CatalogSpecParams catalogSpecParams)
        {

            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter &= builder.Where(p => p.Name.Contains(catalogSpecParams.Search, StringComparison.CurrentCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                var brandfilter = builder.Eq(p => p.Brands.Id, catalogSpecParams.BrandId);
                filter &= brandfilter;
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                var typefilter = builder.Eq(p => p.Brands.Id, catalogSpecParams.TypeId);
                filter &= typefilter;
            }
            var totalItems = await _context.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecParams, filter);            
            return new Pagination<Product>(catalogSpecParams.PageIndex, catalogSpecParams.PageSize, (int)totalItems, data);
        }
                
        async Task<IEnumerable<Product>> IProductRepository.GetProductsByBrand(string BrandName)
        {
            return await _context
                .Products
                .Find(x => x.Brands.Name.Equals(BrandName, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsByName(string Name)
        {
            return await _context
                .Products
                .Find(filter: x => x.Name.Equals(Name, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }

        async Task<bool> IProductRepository.UpdateProduct(Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            var sortDfn = Builders<Product>.Sort.Ascending("Name"); // Default
            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                sortDfn = catalogSpecParams.Sort switch
                {
                    "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                    "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                    _ => Builders<Product>.Sort.Ascending(p => p.Name),
                };
            }
            return await _context.Products
                .Find(filter)
                .Sort(sortDfn)
                .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                .Limit(catalogSpecParams.PageSize).ToListAsync();
        }

    }
}
