using Ambev.DeveloperEvaluation.Application.Messaging;
using Ambev.DeveloperEvaluation.Application.Sale.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using Ambev.DeveloperEvaluation.Application.Sale.ListSale;
using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IMessageService _messageService;

    public SalesController(IMediator mediator, IMapper mapper, IMessageService messageService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _messageService = messageService;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSale.CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var itemsValidator = new CreateSale.CreateSaleItemValidator();
        foreach (var item in request.Items)
        {
            var itemValidationResult = await itemsValidator.ValidateAsync(item, cancellationToken);
            if (!itemValidationResult.IsValid)
                return BadRequest(itemValidationResult.Errors);
        }

        var command = _mapper.Map<CreateSaleCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        await _messageService.SendMessageAsync($"Sale created with Number: {response.SaleNumber}");

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(response)
        });
    }

    /// <summary>
    /// Updates an existing sale
    /// </summary>
    /// <param name="request">The sale update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(int id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var itemsValidator = new UpdateSaleItemRequestValidator();
        foreach (var item in request.Items)
        {
            var itemValidationResult = await itemsValidator.ValidateAsync(item, cancellationToken);
            if (!itemValidationResult.IsValid)
                return BadRequest(itemValidationResult.Errors);
        }

        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;  // Ensuring the SaleId is set for the update

        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
        {
            return NotFound(new ApiResponse { Success = false, Message = "Sale not found" });
        }

        await _messageService.SendMessageAsync($"Sale updated with Number: {response.SaleNumber}");

        return Ok(new ApiResponseWithData<UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = _mapper.Map<UpdateSaleResponse>(response)
        });
    }

    /// <summary>
    /// Cancel a sale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the sale was cancelled</returns>
    [HttpPut("{id}/cancel")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new CancelSaleRequest { Id = id };
        var validator = new CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CancelSaleCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale cancelled successfully"
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale(int id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { Id = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetSaleCommand(id);

        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
            return NotFound();

        var result = _mapper.Map<GetSaleResult>(response);

        return Ok(new ApiResponseWithData<GetSaleResult>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = result
        });
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<ListSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListSale(CancellationToken cancellationToken)
    {
        var request = new ListSaleRequest();
        var validator = new ListSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new ListSaleCommand();

        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
            return NotFound();

        var result = _mapper.Map<ListSaleResult>(response);

        return Ok(new ApiResponseWithData<ListSaleResult>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = result
        });
    }
}
