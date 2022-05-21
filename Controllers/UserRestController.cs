using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using notebook.Services;
using notebook.Model;
using notebook.Util;
using notebook.Dto;

namespace notebook.Controllers;

[ApiController]
[Route("base/rest/admin/users")]
public class UserRestController : ControllerBase
{
    private readonly ILogger<UserRestController> _logger;

    private IUserService<User> _userService;

    public UserRestController(
        ILogger<UserRestController> logger,
        IUserService<User> userService
    )
    {
        _logger = logger;
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet]
    public IEnumerable<User> Get()
    {
        return _userService.Get();
    }

    [Authorize(Roles = "ROLE_ADMIN")]
    [HttpPost]
    public void Post([FromBody] UserDto userDto)
    {
        UserRole userRole = new UserRole{ role = Role.ROLE_USER};
        List<UserRole> userRoles = new List<UserRole>();
        userRoles.Add(userRole);
        User user = new User(null, userDto.name ?? String.Empty, userDto.email, userDto.password, userRoles);
        _userService.Create(user);
    }

    [Authorize(Roles = "ROLE_ADMIN")]
    [Route("{id}")]
    [HttpPut]
    public void Put([FromBody] UserDto userDto)
    {   
        User user = _userService.FindById(userDto.id ?? 0);
        user = UserUtil.getUserFromUserDtoAndRepositoryUser(userDto, user);
        _userService.Create(user);
    }

    [Authorize(Roles = "ROLE_ADMIN")]
    [Route("{id}")]
    [HttpDelete]
    public void Delete(int id)
    {   
        User user = _userService.FindById(id);
        _userService.Remove(user);
    }


}
