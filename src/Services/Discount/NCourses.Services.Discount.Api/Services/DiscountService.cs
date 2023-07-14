using System.Data;
using System.Net;

using Dapper;

using NCourses.Shared.Dtos;

using Npgsql;

namespace NCourses.Services.Discount.Api.Services;

public class DiscountService : IDiscountService
{
    private readonly IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
    }

    public async Task<Response<List<Models.Discount>>> GetAllAsync()
    {
        var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discounts");
        return Response<List<Models.Discount>>.Success(discounts.ToList());
    }

    public async Task<Response<List<Models.Discount>>> GetAllByUserIdAsync(string userId)
    {
        var discounts = await _dbConnection
            .QueryAsync<Models.Discount>("select * from discounts where userid=@UserId", new { UserId = userId });

        return Response<List<Models.Discount>>.Success(discounts.ToList());
    }

    public async Task<Response<Models.Discount>> GetByIdAsync(int id)
    {
        var discount = await _dbConnection
            .QueryFirstOrDefaultAsync<Models.Discount>("select * from discounts where id=@Id", new { Id = id });

        if (discount is null)
            return Response<Models.Discount>.Fail("Discount not found", HttpStatusCode.NotFound);

        return Response<Models.Discount>.Success(discount);
    }

    public async Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId)
    {
        var discount = await _dbConnection
            .QueryFirstOrDefaultAsync<Models.Discount>("select * from discounts where code=@Code and userid=@UserId",
                new { Code = code, UserId = userId });

        if (discount is null)
            return Response<Models.Discount>.Fail("Discount not found", HttpStatusCode.NotFound);

        return Response<Models.Discount>.Success(discount);
    }

    public async Task<Response<NoContentResponse>> SaveAsync(Models.Discount discount)
    {
        var status = await _dbConnection
            .ExecuteAsync("insert into discounts (userid, rate, code) values (@UserId, @Rate, @Code)", discount);

        if (status <= 0)
            return Response<NoContentResponse>.Fail("An error occurred while adding discount",
                HttpStatusCode.InternalServerError);

        return Response<NoContentResponse>.Success(HttpStatusCode.NoContent);
    }

    public async Task<Response<NoContentResponse>> UpdateAsync(Models.Discount discount)
    {
        var discountExists = await _dbConnection
            .ExecuteScalarAsync<bool>("select 1 from discounts where id=@Id", new { Id = discount.Id });

        if (!discountExists)
            return Response<NoContentResponse>.Fail("Discount not found", HttpStatusCode.NotFound);

        var status = await _dbConnection
            .ExecuteAsync("update discounts set userid=@UserId, rate=@Rate, code=@Code where id=@Id", discount);

        if (status <= 0)
            return Response<NoContentResponse>.Fail("An error occurred while updating discount",
                HttpStatusCode.InternalServerError);

        return Response<NoContentResponse>.Success(HttpStatusCode.NoContent);
    }

    public async Task<Response<NoContentResponse>> DeleteAsync(int id)
    {
        var discountExists =
            await _dbConnection.ExecuteScalarAsync<bool>("select 1 from discounts where id=@Id", new { Id = id });

        if (!discountExists)
            return Response<NoContentResponse>.Fail("Discount not found", HttpStatusCode.NotFound);

        var status = await _dbConnection.ExecuteAsync("delete from discounts where id=@Id", new { Id = id });

        if (status <= 0)
            return Response<NoContentResponse>.Fail("An error occurred while deleting discount",
                HttpStatusCode.InternalServerError);

        return Response<NoContentResponse>.Success(HttpStatusCode.NoContent);
    }
}