using System.Net;

using AutoMapper;

using MongoDB.Driver;

using NCourses.Services.Catalog.Api.Configuration;
using NCourses.Services.Catalog.Api.Dtos;
using NCourses.Services.Catalog.Api.Models;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Catalog.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(
        IMapper mapper,
        IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(_ => true).ToListAsync();
        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string categoryId)
    {
        var category = await _categoryCollection.Find(p => p.Id == categoryId).FirstOrDefaultAsync();

        if (category is null)
            return Response<CategoryDto>.Fail("Category not found", HttpStatusCode.NotFound);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto input)
    {
        var category = _mapper.Map<Category>(input);
        await _categoryCollection.InsertOneAsync(category);
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), HttpStatusCode.Created);
    }
    
    public async Task<Response<NoContentResponse>> UpdateAsync(CategoryUpdateDto input)
    {
        var updateCategory = _mapper.Map<Category>(input);
        var document = await _categoryCollection.FindOneAndReplaceAsync(p => p.Id == updateCategory.Id, updateCategory);
        if (document is null)
            return Response<NoContentResponse>.Fail("Category not found", HttpStatusCode.NotFound);
        return Response<NoContentResponse>.Success(HttpStatusCode.NoContent);
    }

    public async Task<Response<NoContentResponse>> DeleteAsync(string categoryId)
    {
        var result = await _categoryCollection.DeleteOneAsync(x => x.Id == categoryId);
        if (result.DeletedCount <= 0)
            return Response<NoContentResponse>.Fail("Category not found", HttpStatusCode.NotFound);
        return Response<NoContentResponse>.Success(HttpStatusCode.NoContent);
    }
}