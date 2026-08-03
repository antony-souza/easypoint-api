using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Categories;

[ApiController]
[Route("/categories")]
public class CategoryController(ISender mediator) : ControllerBase
{
}