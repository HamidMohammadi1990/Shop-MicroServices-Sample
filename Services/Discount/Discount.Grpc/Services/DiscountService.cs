using Grpc.Core;
using Discount.Grpc.Protos;
using Discount.Grpc.Entities;
using Discount.Grpc.Repositories;

namespace Discount.Grpc.Services;

public class DiscountService
    (IDiscountRepository discountRepository)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountRepository.GetDiscount(request.ProductName);
        var response = new GetDiscountResponse
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Amount = coupon.Amount,
            Description = coupon.Description
        };
        return response;
    }

    public override async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var isSuccess = await discountRepository.CreateDiscount(new Coupon
        {
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description
        });
        return new CreateDiscountResponse { Success = isSuccess };
    }

    public override async Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var isSuccess = await discountRepository.UpdateDiscount(new Coupon
        {
            Id = request.Coupon.Id,
            ProductName = request.Coupon.ProductName,
            Amount = request.Coupon.Amount,
            Description = request.Coupon.Description
        });
        return new UpdateDiscountResponse { Success = isSuccess };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var isSuccess = await discountRepository.DeleteDiscount(request.ProductName);
        return new DeleteDiscountResponse { Success = isSuccess };
    }
}