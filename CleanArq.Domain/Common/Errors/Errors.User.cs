using ErrorOr;

namespace CleanArq.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");

        public static Error NotAuthenticated => Error.Unexpected(
            code: "User.NotAuthenticated",
            description: "An unexpected error has occurred finding user authenticated.");

        public static Error ProblemUpserttingAddress => Error.Unexpected(
            code: "User.ProblemUpserttingAddress",
            description: "An unexpected error has occurred upsertting address.");
    }
}