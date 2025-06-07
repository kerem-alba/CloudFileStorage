using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.AuthApi.CQRS.User.Queries
{
    public record GetUsersByIdsQuery(List<int> Ids) : IRequest<ServiceResponse<List<UserBasicDto>>>;

}
