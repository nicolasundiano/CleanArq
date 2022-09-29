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
            code: "User.Unexpected",
            description: "An unexpected error has occurred finding user authenticated.");

        public static Error UpserttingAddress => Error.Unexpected(
            code: "User.SettingAddress",
            description: "An unexpected error has occurred upsertting address.");
    }
}