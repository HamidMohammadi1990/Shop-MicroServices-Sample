using Discount.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Discount.Api.Repositories;

namespace Discount.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController(IDiscountRepository discountRepository) : ControllerBase
{
    private readonly IDiscountRepository discountRepository = discountRepository;

    [HttpGet("{productName}", Name = "get-discount")]
    public async Task<Coupon?> GetDiscount(string productName)
    {
        var discount = await discountRepository.GetDiscount(productName);
        return discount;
    }

    [HttpPost("create")]
    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        var affected = await discountRepository.CreateDiscount(coupon);
        return affected;
    }

    [HttpPut("update")]
    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        var affected = await discountRepository.UpdateDiscount(coupon);
        return affected;
    }

    [HttpDelete("delete")]
    public async Task<bool> DeleteDiscount(string productName)
    {
        var affected = await discountRepository.DeleteDiscount(productName);
        return affected;
    }
}