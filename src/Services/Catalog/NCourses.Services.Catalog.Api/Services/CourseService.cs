using System.Net;

using AutoMapper;

using MongoDB.Driver;

using NCourses.Services.Catalog.Api.Configuration;
using NCourses.Services.Catalog.Api.Dtos;
using NCourses.Services.Catalog.Api.Models;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Catalog.Api.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(_ => true).ToListAsync();

        await FillCategories(courses);

        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses));
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string courseId)
    {
        var course = await _courseCollection.Find(x => x.Id == courseId).FirstOrDefaultAsync();

        if (course == null)
            return Response<CourseDto>.Fail("Course not found", HttpStatusCode.NotFound);

        await FillCategory(course);

        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course));
    }

    public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
    {
        var courses = await _courseCollection.Find(course => course.UserId == userId).ToListAsync();

        await FillCategories(courses);

        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses));
    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto input)
    {
        var course = _mapper.Map<Course>(input);

        var categoryExists = await _categoryCollection.Find(x => x.Id == course.CategoryId).AnyAsync();

        if (!categoryExists)
            return Response<CourseDto>.Fail("Category not found with that id: " + course.CategoryId,
                HttpStatusCode.NotFound);

        // TODO: set user id

        course.CreatedTime = DateTime.Now;

        await _courseCollection.InsertOneAsync(course);

        try
        {
            await FillCategory(course);
        }
        catch (Exception)
        {
            // ignored
        }

        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), HttpStatusCode.Created);
    }

    public async Task<Response<NoContentResponse>> UpdateAsync(CourseUpdateDto input)
    {
        var course = _mapper.Map<Course>(input);
        var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == input.Id, course);

        if (result == null)
            return Response<NoContentResponse>.Fail("Course not found.", HttpStatusCode.NotFound);

        return Response<NoContentResponse>.Success(HttpStatusCode.NoContent);
    }

    public async Task<Response<NoContentResponse>> DeleteAsync(string courseId)
    {
        var result = await _categoryCollection.DeleteOneAsync(x => x.Id == courseId);

        if (result?.DeletedCount > 0)
            return Response<NoContentResponse>.Success(HttpStatusCode.NoContent);

        return Response<NoContentResponse>.Fail("Course not found.", HttpStatusCode.NotFound);
    }

    private async Task FillCategories(List<Course> courses)
    {
        foreach (var course in courses)
        {
            await FillCategory(course);
        }
    }

    private async Task FillCategory(Course course)
    {
        course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
    }
}