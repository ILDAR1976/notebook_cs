using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using notebook.Model; // класс Person
using notebook.Dto; 
using notebook.Util; 
using notebook.Services; 
namespace notebook.Controllers.Secutiry
{
    [ApiController]
    public class AccountController : Controller
    {

        private ClaimsIdentity? identity; 
        
        private IUserService<User> _userService;

        public AccountController(
            IUserService<User> userService

        ){
            this._userService = userService;
        }

        [Route("/base/user/whoami")]
        [HttpPost]
        public int Whoami()
        {
            User curentUser = _userService.getCurrentUser();    
            
            IEnumerable<UserRole> userRole = curentUser.roles.Where(x => x.role == Role.ROLE_ADMIN).ToList();

            if (userRole == null) return 0;
            if (userRole.Count() == 0) {
                return 0;    
            } else {
                return 1;
            }

        }

        [Route("/base/api/auth/login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            identity = GetIdentity(user.email, user.password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
 
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
 
            return Json(response);
        }
 
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = _userService.getUserByEmail(username);    
            
            if (user == null ) {
                return null;
            }

            Microsoft.AspNetCore.Identity.PasswordVerificationResult result
                = Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;  

            if (user.password.Contains("{bcrypt}")) {
                
                result =  BCrypt.Net.BCrypt.Verify(password, user.password.Replace("{bcrypt}","")) ?
                    Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success:
                    Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;    

                if (result == null) {
                    result = Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;    
                }
            } else if (user.password.Contains("{noop}")) {
                if (password.Equals(user.password.Replace("{noop}",""))) {
                    result = Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
                } else {
                    result = Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;    
                }
            }

            if (user != null && 
                result.Equals(Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.email)
                };

                foreach (var role in user.roles) {
                   claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString()));
                }

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
 
            // если пользователя не найдено
            return null;
        }


        [Route("/base/api/auth/logout")]
        [HttpPost]
        public void Logout() {
           identity = null; 
        }

        [Route("/base/user/register")]
        [HttpPost]
        public void Register([FromBody] UserDto userDto) {
            User user = UserUtil.getUserFromUserDto(userDto);
            user.id = null;
            _userService.Create(user);
        }
    }
}