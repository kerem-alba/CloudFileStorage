using CloudFileStorage.AuthApi.CQRS.User.Queries;
using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.AuthApi.Repositories;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.AuthApi.CQRS.User.Handlers
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ServiceResponse<List<UserListDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<List<UserListDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            var dtoList = users.Select(u => new UserListDto
            {
                Id = u.Id,
                Email = u.Email
            }).ToList();

            return new ServiceResponse<List<UserListDto>>
            {
                Data = dtoList,
                Success = true,
                StatusCode = 200,
                Message = ResponseMessages.UsersFetched
            };
        }
    }
}
