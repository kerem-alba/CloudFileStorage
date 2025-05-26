using Microsoft.AspNetCore.Mvc;
using SMediator.Core.Abstractions;
using CloudFileStorage.Common.Extensions;
using CloudFileStorage.AuthApi.CQRS.User.Queries;


namespace CloudFileStorage.AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserNameById(int id)
        {
            var response = await _mediator.Send(new GetUserNameByIdQuery(id));
            return this.HandleResponse(response);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUserList()
        {
            var response = await _mediator.Send(new GetUserListQuery());
            return this.HandleResponse(response);
        }
    }

}
