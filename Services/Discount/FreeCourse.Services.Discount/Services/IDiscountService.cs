using FreeCourse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Models.Discount>>> GetAll();
        Task<Response<Models.Discount>> GetById(int id);
        Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userid);
        Task<Response<NoContent>> Add(Models.Discount model);
        Task<Response<NoContent>> Update(Models.Discount model);
        Task<Response<NoContent>> Delete(int id);
    }
}
