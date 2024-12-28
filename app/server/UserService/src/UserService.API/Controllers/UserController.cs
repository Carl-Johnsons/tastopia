using Microsoft.AspNetCore.Mvc;
using UserService.API.DTOs;
using UserService.Application.Users.Commands;

namespace UserService.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseApiController
    {
        public UserController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
        {
        }

        [HttpPost("search-user")]
        public async Task<IActionResult> SearchUser([FromBody] SearchUser searchUser)
        {
    

            var result = await _sender.Send(new SearchUsersCommand
            {
                UserId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
                Skip = searchUser.Skip,
                Keyword = searchUser.Keyword,
            });
            result.ThrowIfFailure();
            return Ok(result.Value);
        }
    }
}
