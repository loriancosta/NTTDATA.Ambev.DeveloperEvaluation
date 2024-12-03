using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations
    /// </summary>
    public interface ISaleRepository
    {
        
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<Sale?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }

    public interface ISaleItemRepository
    {
        Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);
        Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);

        Task<SaleItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
