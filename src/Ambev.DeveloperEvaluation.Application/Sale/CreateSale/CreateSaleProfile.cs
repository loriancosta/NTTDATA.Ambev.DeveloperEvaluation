using Ambev.DeveloperEvaluation.Application.Messaging.SaleEvents;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Domain.Entities.Sale>();
        CreateMap<CreateSaleItemCommand, Domain.Entities.SaleItem>();
        CreateMap<Domain.Entities.Sale, CreateSaleResult>();
        CreateMap<Domain.Entities.SaleItem, CreateSaleItemResult>();
        CreateMap<CreateSaleResult, SaleCreatedEvent>();
        CreateMap<Domain.Entities.Sale, SaleCreatedEvent>();

    }


}
