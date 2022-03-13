using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourceService : ICourceService
    {
        private readonly IMongoCollection<Course> _courceCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourceService(IMapper mapper, ICategoryService categoryService, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courceCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var cources = await _courceCollection.Find(cource => true).ToListAsync();

            if (cources.Any())
            {
                foreach (var course in cources)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
                return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(cources), StatusCodes.Status200OK);
            }

            return Response<List<CourseDto>>.Success(new List<CourseDto>(), StatusCodes.Status204NoContent);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var cources = await _courceCollection.Find(x => x.UserId == userId).ToListAsync();

            if (cources.Any())
            {
                foreach (var course in cources)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                cources = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(cources), StatusCodes.Status200OK);

        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course != null)
            {
                course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), StatusCodes.Status200OK);
            }

            return Response<CourseDto>.Fail("Course not found", StatusCodes.Status404NotFound);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto course)
        {
            var mappedCourse = _mapper.Map<Course>(course);
            mappedCourse.CreatedTime = DateTime.Now;

            await _courceCollection.InsertOneAsync(mappedCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(mappedCourse), StatusCodes.Status201Created);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto course)
        {
            var mappingCourse = _mapper.Map<Course>(course);
            var result = await _courceCollection.FindOneAndReplaceAsync(x => x.Id == course.Id, mappingCourse);
            return result == null ? Response<NoContent>.Fail("Course not found", StatusCodes.Status404NotFound) : Response<NoContent>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courceCollection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0 ? Response<NoContent>.Success(StatusCodes.Status204NoContent) : Response<NoContent>.Fail("Course not found", StatusCodes.Status404NotFound);
        }
    }
}
