using Peyghom.Common.Domain;

namespace Peyghom.Modules.Users.Domain;

public static class Errors
{
    public static readonly Error OtpExist = Error.Problem(
        "Users.OtpExist",
        "Cannot send code because you have an ongoing request, wait till the code expires");


    public static readonly Error NoRoleAssigned = Error.Failure(
        "Users.NoRoleAssigned",
        "Sorry, we cannot proceed your request. You have no role in the system.");

    public static readonly Error NoMatchingRole = Error.Failure(
        "Users.NoMatchingRole",
        "No matching roles found for the user.");


    public static Error NotFound(string userId) =>
        Error.NotFound("Users.NotFound", $"The user with the identifier {userId} not found");
}
