using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices;

public class DiscountGrpcService
    (DiscountProtoService.DiscountProtoServiceClient discountProtoService)
{
    public async Task<GetDiscountResponse> GetDiscount(string productName)
    {
        var request = new GetDiscountRequest { ProductName = productName };
        var response = await discountProtoService.GetDiscountAsync(request);
        return response;
    }
}