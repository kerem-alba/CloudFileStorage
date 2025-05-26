using CloudFileStorage.AuthApi.CQRS.User.Queries;
using CloudFileStorage.AuthApi.Repositories;
using CloudFileStorage.AuthApi.Services.Interfaces;
using CloudFileStorage.Common.Models;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.AuthApi.CQRS.User.Handlers
{
    public class GetUserNameByIdQueryHandler : IRequestHandler<GetUserNameByIdQuery, ServiceResponse<string>>
    {
        private readonly IUserService _userService;

        public GetUserNameByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<string>> Handle(GetUserNameByIdQuery request, CancellationToken ct)
        {
            return await _userService.GetUserNameByIdAsync(request.Id);
        }
    }

}
