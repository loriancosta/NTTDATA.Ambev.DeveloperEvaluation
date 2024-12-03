using Ambev.DeveloperEvaluation.Application.Sale.ListSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;

public class ListSaleProfile : Profile
{
    public ListSaleProfile()
    {
        CreateMap<ListSaleRequest, ListSaleCommand>();
        CreateMap<ListSaleResult, ListSaleResponse>();
        CreateMap<List<Sale>, ListSaleResult>().ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src));
    }
}
