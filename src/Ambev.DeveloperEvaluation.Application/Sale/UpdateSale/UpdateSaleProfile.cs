using Ambev.DeveloperEvaluation.Application.Messaging.SaleEvents;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Domain.Entities.Sale>();
        CreateMap<UpdateSaleItemCommand, Domain.Entities.SaleItem>();
        CreateMap<Domain.Entities.Sale, UpdateSaleResult>();
        CreateMap<Domain.Entities.SaleItem, UpdateSaleItemResult>();
        CreateMap<UpdateSaleResult, SaleUpdatedEvent>();
        CreateMap<Domain.Entities.Sale, SaleUpdatedEvent>();

    }


}
