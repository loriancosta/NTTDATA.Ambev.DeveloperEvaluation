using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Messaging;
using Ambev.DeveloperEvaluation.Application.Messaging.SaleEvents;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public CreateSaleHandler(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Domain.Entities.Sale>(command);

        foreach (var itemCommand in command.Items)
        {
            var saleItem = _mapper.Map<Domain.Entities.SaleItem>(itemCommand);
            sale.AddItem(saleItem);  // <-- Add item to sale
        }

        decimal totalAmount = 0;

        foreach (var item in sale.SaleItems)
        {
            item.CalculateDiscount();
            item.CalculateTotalAmount();
            totalAmount += item.TotalAmount;
        }

        sale.TotalAmount = totalAmount;

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(createdSale);

        var saleCreatedEvent = _mapper.Map<SaleCreatedEvent>(createdSale);
        await _eventPublisher.PublishEventAsync(saleCreatedEvent);

        return result;
    }
}
