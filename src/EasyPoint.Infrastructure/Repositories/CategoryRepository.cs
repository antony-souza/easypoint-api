using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;

namespace EasyPoint.Infrastructure.Repositories;

public class CategoryRepository(EasyPointDbContext context) : Repository<Category>(context), ICategoryRepository;