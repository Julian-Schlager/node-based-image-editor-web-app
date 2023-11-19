using Microsoft.AspNetCore.Mvc;
using NodeEditor.BuisnessLogic.Implementation;
using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DTO;
using NodeEditor.Entities;
using NodeEditor.RestAPI.UserManagment;

namespace NodeEditor.RestAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;

            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterData data)
        {
            try
            {
                User user = await userService.Register(data);

                if (user == null)
                {
                    return BadRequest(new { message = "User could not be created" });
                }

                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost( "LogIn")]
        public async Task<IActionResult> LogIn([FromBody] AuthenticateModel model)
        {
            User user = await userService.LogIn(model.Email, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }



        //[HttpPost(Name = "LogOut")]
        //public async Task<bool> LogOut()
        //{
        //    return await this.userService.
        //}

        [HttpPost("DeleteAccount")]
        public async Task<bool> DeleteAccount(string email)
        {
            return await this.userService.DeleteAccount(email);
        }
    }
}