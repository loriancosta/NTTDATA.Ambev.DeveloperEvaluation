using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Repository implementation for Sale entity operations
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await _context.Sales.AddAsync(sale, cancellationToken);

                foreach (var item in sale.SaleItems)
                {
                    item.SaleId = sale.Id;
                    await _context.SaleItems.AddAsync(item, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return sale;
            }
            catch (Exception e)
            {
                var erro = e.Message;
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Sale?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            return sale;
        }


        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sales = await _context.Sales
                .Include(s => s.SaleItems)
                .ToListAsync(cancellationToken);

            return sales;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                _context.Sales.Update(sale);

                if (!sale.IsCancelled) // If it's a cancellation, just save the sale
                {
                    foreach (var item in sale.SaleItems)
                    {
                        var existingItem = await _context.SaleItems.FirstOrDefaultAsync(si => si.Id == item.Id, cancellationToken);

                        if (existingItem != null)
                        {
                            _context.SaleItems.Update(item);
                        }
                        else
                        {
                            item.SaleId = sale.Id;
                            await _context.SaleItems.AddAsync(item, cancellationToken);
                        }
                    }

                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return sale;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Error updating sale: {e.Message}", e);
            }
        }
    }

    /// <summary>
    /// Repository implementation for SaleItem entity operations
    /// </summary>
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly DefaultContext _context;

        public SaleItemRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            _context.SaleItems.Add(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return saleItem;
        }

        public async Task<SaleItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .FirstOrDefaultAsync(si => si.SaleId == id, cancellationToken);
        }

        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .Where(si => si.SaleId == saleId)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var saleItem = await _context.SaleItems.FindAsync(id);
            if (saleItem == null) return false;

            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
