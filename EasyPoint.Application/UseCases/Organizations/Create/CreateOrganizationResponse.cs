namespace EasyPoint.Application.UseCases.Organizations.Create;

public sealed record CreateOrganizationResponse(
    Guid Id,
    string Name,
    string Cnpj
);