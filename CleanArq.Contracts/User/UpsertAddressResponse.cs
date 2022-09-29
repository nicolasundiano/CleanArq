namespace CleanArq.Contracts.User;

public record UpsertAddressResponse(
    int Id,
    string? Street,
    string? City,
    string? Country);
