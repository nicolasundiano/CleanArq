namespace CleanArq.Contracts.User;

public record UpsertAddressRequest(
    string? Street,
    string? City,
    string? Country);
