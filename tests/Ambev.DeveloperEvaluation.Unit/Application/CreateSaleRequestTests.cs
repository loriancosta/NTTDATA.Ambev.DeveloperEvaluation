using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleRequestTests
{
    [Fact]
    public void Should_Create_SaleRequest_With_Valid_Data()
    {
        // Arrange: Prepare a valid CreateSaleRequest
        var request = new CreateSaleRequest
        {
            CustomerId = 1,
            BranchId = 1,
            IsCancelled = false,
            Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductName = "Product xUnit 1",
                        Quantity = 2,
                        UnitPrice = 10.0m
                    },
                    new CreateSaleItemRequest
                    {
                        ProductName = "Product xUnit 2",
                        Quantity = 1,
                        UnitPrice = 20.0m
                    }
                }
        };

        // Act: Validate the CreateSaleRequest using a validator
        var validator = new CreateSaleValidator();
        var result = validator.Validate(request);

        // Assert: Ensure validation is successful
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_CustomerId_Is_Zero()
    {
        // Arrange: Prepare CreateSaleRequest with an invalid CustomerId
        var request = new CreateSaleRequest
        {
            CustomerId = 0,  // Invalid CustomerId
            BranchId = 1,
            IsCancelled = false,
            Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductName = "Product xUnit 1",
                        Quantity = 2,
                        UnitPrice = 10.0m
                    }
                }
        };

        // Act: Validate the CreateSaleRequest
        var validator = new CreateSaleValidator();
        var result = validator.Validate(request);

        // Assert: Ensure validation fails for CustomerId
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CustomerId");
    }

    [Fact]
    public void Should_Fail_When_BranchId_Is_Zero()
    {
        // Arrange: Prepare CreateSaleRequest with an invalid BranchId
        var request = new CreateSaleRequest
        {
            CustomerId = 1,
            BranchId = 0,  // Invalid BranchId
            IsCancelled = false,
            Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductName = "Product xUnit 1",
                        Quantity = 2,
                        UnitPrice = 10.0m
                    }
                }
        };

        // Act: Validate the CreateSaleRequest
        var validator = new CreateSaleValidator();
        var result = validator.Validate(request);

        // Assert: Ensure validation fails for BranchId
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "BranchId");
    }

    [Fact]
    public void Should_Fail_When_Empty_Item_List()
    {
        // Arrange: Prepare CreateSaleRequest with empty items
        var request = new CreateSaleRequest
        {
            CustomerId = 1,
            BranchId = 1,
            IsCancelled = false,
            Items = new List<CreateSaleItemRequest>()  // Empty Items
        };

        // Act: Validate the CreateSaleRequest
        var validator = new CreateSaleValidator();
        var result = validator.Validate(request);

        // Assert: Ensure validation fails because the Items list is empty
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Items");
    }

    [Fact]
    public void Should_Create_Item_With_Valid_Data()
    {
        // Arrange: Prepare valid CreateSaleItemRequest
        var item = new CreateSaleItemRequest
        {
            ProductName = "Product 1",
            Quantity = 2,
            UnitPrice = 10.0m
        };

        // Act: Validate the CreateSaleItemRequest using the validator
        var validator = new CreateSaleItemValidator();
        var result = validator.Validate(item);

        // Assert: Ensure validation is successful
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_Item_Quantity_Is_Zero()
    {
        // Arrange: Prepare invalid CreateSaleItemRequest with zero quantity
        var item = new CreateSaleItemRequest
        {
            ProductName = "Product xUnit 1",
            Quantity = 0,  // Invalid Quantity
            UnitPrice = 10.0m
        };

        // Act: Validate the CreateSaleItemRequest
        var validator = new CreateSaleItemValidator();
        var result = validator.Validate(item);

        // Assert: Ensure validation fails for Quantity
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Quantity");
    }

    [Fact]
    public void Should_Fail_When_Item_UnitPrice_Is_Zero()
    {
        // Arrange: Prepare invalid CreateSaleItemRequest with zero unit price
        var item = new CreateSaleItemRequest
        {
            ProductName = "Product xUnit 1",
            Quantity = 1,
            UnitPrice = 0  // Invalid UnitPrice
        };

        // Act: Validate the CreateSaleItemRequest
        var validator = new CreateSaleItemValidator();
        var result = validator.Validate(item);

        // Assert: Ensure validation fails for UnitPrice
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UnitPrice");
    }

}
