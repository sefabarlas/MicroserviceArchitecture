using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseViewModel> GetCourseByIdAsync(string courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCourseAsync()
        {
            throw new NotImplementedException();
        }   
    }
}
