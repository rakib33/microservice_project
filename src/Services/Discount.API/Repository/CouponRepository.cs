using Dapper;
using Discount.API.Models;
using Npgsql;

namespace Discount.API.Repository
{
    public class CouponRepository : ICouponRepository
    {

           IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE  ProductId=@ProductId",new { ProductId = productId });

            if (coupon == null)
            {
                return new Coupon {ProductName = "No Discount", Amount = 0 };
            }

            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
         var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
         var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductId, ProductName, Description, Amount) VALUES (@ProductId,@ProductName, @Description, @Amount)", new { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
           var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var affected = connection.ExecuteAsync("UPDATE Coupon SET ProductId=@ProductId, ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id", new { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected != null)
            {
                return true;
            }

            return false;

        }
        public async Task<bool> DeleteDiscount(string productId)
        {
             var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var affected = connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductId=@ProductId", new { ProductId  = productId });

            if (affected != null)
            {
                return true;
            }
            return false;
        }




    }
}
