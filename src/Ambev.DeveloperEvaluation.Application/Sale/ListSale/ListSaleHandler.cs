using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSale;

/// <summary>
/// Handler for processing ListSaleCommand requests
/// </summary>
public class ListSaleHandler : IRequestHandler<ListSaleCommand, ListSaleResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListSaleHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for ListSaleCommand</param>
    public ListSaleHandler(ISaleRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListSaleCommand request
    /// </summary>
    /// <param name="request">The ListSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<ListSaleResult> Handle(ListSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sales = await _repository.GetAllAsync(cancellationToken);

        if (sales == null)
            throw new NotFoundException("Sales not found.");

        var result = _mapper.Map<ListSaleResult>(sales);

        return result;
    }
}
