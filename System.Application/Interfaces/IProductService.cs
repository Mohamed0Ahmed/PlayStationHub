using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsByBranchAsync(int branchId);
        Task<Product> CreateProductAsync(int branchId, string name, decimal price);
        Task<Product> UpdateProductAsync(int productId, string name, decimal price);
        Task<bool> DeleteProductAsync(int productId);
    }
}