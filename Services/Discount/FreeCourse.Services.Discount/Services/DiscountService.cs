using Dapper;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostreSql"));
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discount = await _connection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discount.ToList(), StatusCodes.Status200OK);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE id = @Id", new { Id = id })).SingleOrDefault();

            return discount == null ? Response<Models.Discount>.Fail("Discount not found", StatusCodes.Status404NotFound) : Response<Models.Discount>.Success(discount, StatusCodes.Status200OK);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userid)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE userid = @UserId AND code = @Code", new { UserId = userid, Code = code })).SingleOrDefault();

            return discount == null ? Response<Models.Discount>.Fail("Discount not found", StatusCodes.Status404NotFound) : Response<Models.Discount>.Success(discount, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> Add(Models.Discount model)
        {
            var status = await _connection.ExecuteAsync("INSERT INTO discount(userid, rate, code) VALUES(@UserId, @Rate, @Code)", model);

            return status > 0 ? Response<NoContent>.Success(StatusCodes.Status204NoContent) : Response<NoContent>.Fail("An error accurred while adding", StatusCodes.Status500InternalServerError);
        }

        public async Task<Response<NoContent>> Update(Models.Discount model)
        {
            var status = await _connection.ExecuteAsync("UPDATE discount SET userid = @UserId, code = @Code, rate = @Rate WHERE id = @Id", new { Id = model.Id, UserId = model.UserId, Code = model.Code, Rate = model.Rate });

            return status > 0 ? Response<NoContent>.Success(StatusCodes.Status204NoContent) : Response<NoContent>.Fail("Discount not found", StatusCodes.Status404NotFound);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _connection.ExecuteAsync("DELETE FROM discount WHERE id = @Id", new { Id = id });

            return status > 0 ? Response<NoContent>.Success(StatusCodes.Status204NoContent) : Response<NoContent>.Fail("Discount not found", StatusCodes.Status404NotFound);
        }
    }
}
