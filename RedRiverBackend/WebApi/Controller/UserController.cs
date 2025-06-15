using Microsoft.AspNetCore.Mvc;

using RedRiverApp.Core.Domain.Users;
using RedRiverApp.Core.Domain.LogIns;
using RedRiverApp.WebApi.Converter;


namespace RedRiverApp.WebApi.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserService userService;
        private readonly LogInService logInService;
        private readonly UserConverter converter;

        public UserController(UserService userService, LogInService logInService, UserConverter converter)
        {
            this.userService = userService;
            this.logInService = logInService;
            this.converter = converter;
        }

        [HttpPost("login")]
        public ActionResult<string> LogIn([FromBody] LogIn logIn)
        {
            var token = logInService.IsInloggad(logIn);
            if (token != null)
            {
                return Ok(token);
            }

            return Unauthorized("Okänt användarnamn eller lösenord");
        }

        [HttpPost("newUser")]

        public ActionResult<LogInResponse> Create([FromBody] NewUserRequest newUser)
        {
            var user = userService.Create(newUser);
            var response = converter.ConvertToResponse(user);
            return Ok(response);
        }

    }
}