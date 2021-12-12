using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourceService
    {
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto course);
        Task<Response<NoContent>> DeleteAsync(string id);
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto course);
    }
}