using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSale;

public class ListSaleProfile : Profile
{
    public ListSaleProfile()
    {
        CreateMap<Domain.Entities.Sale, SaleResult>();
        CreateMap<Domain.Entities.SaleItem, SaleItemResult>();
    }
}
