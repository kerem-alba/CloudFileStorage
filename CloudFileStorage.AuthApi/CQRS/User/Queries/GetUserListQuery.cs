using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.AuthApi.CQRS.User.Queries
{
    public record GetUserListQuery : IRequest<ServiceResponse<List<UserListDto>>>;
}
