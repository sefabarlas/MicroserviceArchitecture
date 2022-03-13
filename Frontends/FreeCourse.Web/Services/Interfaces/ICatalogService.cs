using FreeCourse.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseViewModel> GetCourseByIdAsync(string courseId);
        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);
        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput);
        Task<bool> DeleteCourseAsync(string courseId);
    }
}
