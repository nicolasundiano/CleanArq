using ErrorOr;
using MediatR;

namespace CleanArq.Application.Features.UserFeatures.Commands;

public class UpsertAddressCommand : IRequest<ErrorOr<UpsertAddressResult>>
{
    public string UserEmail { get; set; } = null!;
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
    
