using CloudFileStorage.AuthApi.CQRS.User.Queries;
using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.AuthApi.Repositories;
using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.AuthApi.CQRS.User.Handlers
{
    public class GetUsersByIdsQueryHandler : IRequestHandler<GetUsersByIdsQuery, ServiceResponse<List<UserBasicDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByIdsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<List<UserBasicDto>>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByIdsAsync(request.Ids);

            var result = users.Select(u => new UserBasicDto
            {
                Id = u.Id,
                Name = u.Name
            }).ToList();

            return new ServiceResponse<List<UserBasicDto>>
            {
                Data = result,
                Success = true,
                StatusCode = 200,
                Message = "Kullanıcılar başarıyla getirildi."
            };
        }
    }
}
