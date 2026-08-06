using System.Text.Json.Serialization;
using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Auth.Register;

public sealed record RegisterCommand(
    Guid OrganizationId,
    string Name,
    string UserName,
    string Email,
    string Password
) : IRequest<Result<AuthenticationResponse>>;
