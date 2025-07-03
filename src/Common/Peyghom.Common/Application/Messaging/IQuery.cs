using Peyghom.Common.Domain;
using MediatR;

namespace Peyghom.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
