using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Messaging;
using Ambev.DeveloperEvaluation.Application.Messaging.SaleEvents;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public UpdateSaleHandler(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

        if (sale == null)
        {
            throw new NotFoundException($"Sale with ID {command.Id} not found.");
        }

        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _mapper.Map(command, sale);

        foreach (var itemCommand in command.Items)
        {
            var saleItem = sale.SaleItems.FirstOrDefault(si => si.Id == itemCommand.Id);

            if(saleItem != null)
            {
                saleItem.SaleId = command.Id;
                saleItem.ProductName = itemCommand.ProductName;
                saleItem.Quantity = itemCommand.Quantity;
                saleItem.UnitPrice = itemCommand.UnitPrice;
            }
            else if (itemCommand.Id == 0) // It is a new record that was included in the object.
            {
                saleItem = new SaleItem(itemCommand.ProductName, itemCommand.Quantity, itemCommand.UnitPrice)
                {
                    SaleId = command.Id
                };
            }
            else if (itemCommand.Id != 0)
            {
                throw new NotFoundException($"SaleItem with ID {itemCommand.Id} not found.");
            }

            sale.SaleItems.Add(saleItem);
        }

        decimal totalAmount = 0;

        foreach (var item in sale.SaleItems)
        {
            item.CalculateDiscount();
            item.CalculateTotalAmount();
            totalAmount += item.TotalAmount;
        }

        sale.TotalAmount = totalAmount;

        var updateSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        var result = _mapper.Map<UpdateSaleResult>(updateSale);

        var saleUpdateEvent = _mapper.Map<SaleUpdatedEvent>(updateSale);
        await _eventPublisher.PublishEventAsync(saleUpdateEvent);

        return result;
    }
}
