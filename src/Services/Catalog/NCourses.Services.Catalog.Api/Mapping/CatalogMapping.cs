using AutoMapper;

using NCourses.Services.Catalog.Api.Dtos;
using NCourses.Services.Catalog.Api.Models;

namespace NCourses.Services.Catalog.Api.Mapping;

public class CatalogMapping : Profile
{
    public CatalogMapping()
    {
        CreateMap<Course, CourseDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Feature, FeatureDto>().ReverseMap();
        
        CreateMap<CourseCreateDto, CourseDto>();
        CreateMap<CourseUpdateDto, CourseDto>();
        
        CreateMap<CategoryCreateDto, CategoryDto>();
        CreateMap<CategoryUpdateDto, CategoryDto>();
    }
}