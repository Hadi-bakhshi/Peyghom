using Peyghom.Common.Domain;

namespace Peyghom.Modules.Users.Domain;

public static class Errors
{
    public static readonly Error OtpExist = Error.Problem(
        "Users.OtpExist",
        "Cannot send code because you have an ongoing request, wait till the code expires");
}
