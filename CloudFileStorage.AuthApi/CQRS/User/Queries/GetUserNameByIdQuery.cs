using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.AuthApi.CQRS.User.Queries
{
    public record GetUserNameByIdQuery(int Id) : IRequest<ServiceResponse<string>>;

}
