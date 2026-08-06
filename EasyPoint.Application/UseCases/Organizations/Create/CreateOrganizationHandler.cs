using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Organizations;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Organizations.Create;

public class CreateOrganizationHandler(IOrganizationRepository organizationRepository)
    : IRequestHandler<CreateOrganizationCommand, Result<CreateOrganizationResponse>>
{
    public async Task<Result<CreateOrganizationResponse>> Handle(
        CreateOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        var existingOrganization = await organizationRepository.GetByCnpjAsync(request.Cnpj, cancellationToken);

        if (existingOrganization is not null)
        {
            return Result<CreateOrganizationResponse>.Failure(
                "Já existe uma organização cadastrada com este CNPJ.");
        }

        var organization = new Organization
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Cnpj = request.Cnpj,
        };

        var createdStore = await organizationRepository.CreateAsync(
            organization,
            cancellationToken);

        var response = new CreateOrganizationResponse(
            Id: createdStore.Id,
            Name: createdStore.Name,
            Cnpj: createdStore.Cnpj
        );

        return Result<CreateOrganizationResponse>.Success(response);
    }
}