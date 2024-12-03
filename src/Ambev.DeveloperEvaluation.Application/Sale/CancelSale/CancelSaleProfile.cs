using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class CancelSaleProfile : Profile
{
    public CancelSaleProfile()
    {
        CreateMap<Domain.Entities.Sale, CancelSaleResult>();
    }
}
