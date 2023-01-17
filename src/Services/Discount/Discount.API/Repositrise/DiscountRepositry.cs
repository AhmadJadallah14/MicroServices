using Dapper;
using Discount.API.Entity;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.API.Repositrise
{
    public class DiscountRepositry : IDiscountRepositry
    {
        private readonly IConfiguration _Configration;
        public DiscountRepositry(IConfiguration Configration)
        {
            _Configration = Configration;
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            try
            {

                using var connection = new NpgsqlConnection(_Configration.GetValue<string>("DatabaseSettings:ConnectionString"));
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("select * from coupon where productname =@ProductName", new { ProductName = productName });
            if (coupon is null)
                return new Coupon {ProductName = "No Discount" ,Amount = 0,Description = "No Description"};
                return coupon;

            }

            catch (Exception ex)
            {
                return new Coupon();
            }
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_Configration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_Configration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_Configration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
