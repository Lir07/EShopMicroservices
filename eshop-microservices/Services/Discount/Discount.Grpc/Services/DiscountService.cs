using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;
public class DiscountService
	(DiscountContext dbContext, ILogger<DiscountService> logger)
	: DiscountProtoService.DiscountProtoServiceBase
{
	public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		var coupon = await dbContext
			.Coupons
			.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

		if (coupon is null)
			coupon = new Coupon { ProductName = "No discount", Amount = 0, Description = "No Discount Desc" };

		var couponModel = coupon.Adapt<CouponModel>();
		return couponModel;
	}

	public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		try
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if (coupon is null)
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));

			dbContext.Coupons.Add(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount is successfully created. ProductName : {productName}", coupon.ProductName);

			var couponModel = coupon.Adapt<CouponModel>();
			return couponModel;
		}
		catch (Exception e)
		{
			logger.LogInformation("Discount is successfully created. ProductName : {productName}");
			return null;
		}

	}

	public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
	{
		var coupon = request.Adapt<Coupon>();
		if (coupon is null)
			throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));

		dbContext.Coupons.Update(coupon);
		await dbContext.SaveChangesAsync();
		logger.LogInformation("Discount is successfully udpated. ProductName : {productName}", coupon.ProductName);
		var couponModel = coupon.Adapt<CouponModel>();
		return couponModel;

	}

	public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
	{
		var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

		if (coupon is null)
			throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));

		dbContext.Coupons.Remove(coupon);
		await dbContext.SaveChangesAsync();

		logger.LogInformation("Discount is successfully deleted. ProductName : {productName}", request.ProductName);

		return new DeleteDiscountResponse { Success = true };
	}
}
