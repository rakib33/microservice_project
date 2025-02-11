using Discount.API.Models;

namespace Discount.API.Repository
{
    public interface ICouponRepository
    {

        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);

    }
}
