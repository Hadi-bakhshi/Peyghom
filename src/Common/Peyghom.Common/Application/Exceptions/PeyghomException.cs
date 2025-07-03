using Peyghom.Common.Domain;

namespace Peyghom.Common.Application.Exceptions;

public sealed class PeyghomException : Exception
{
    public PeyghomException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
