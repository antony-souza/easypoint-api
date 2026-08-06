using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Organizations.Create;

public sealed record CreateOrganizationCommand(
    string Name,
    string Cnpj
) : IRequest<Result<CreateOrganizationResponse>>;