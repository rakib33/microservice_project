using Discount.grpc.Models;

namespace Discount.grpc.Repository
{
    public interface ICouponRepository
    {

        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);

    }
}
