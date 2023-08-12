using Async_Inn.DTO;
using Async_Inn.Model.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Async_Inn.Model.Services
{
    public class IdentityUserServices :IUser
    {
        private UserManager<ApplicationUser> userManager;
        private JWTTokenServices jwtTokenServices;

        public IdentityUserServices(UserManager<ApplicationUser> manager, JWTTokenServices jwtTokenServices)
        {
            userManager = manager;
            this.jwtTokenServices = jwtTokenServices;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool validPassword = await userManager.CheckPasswordAsync(user, password);

            if (validPassword)
            {
                return new UserDTO { Id = user.Id,
                    Username = user.UserName ,
                    Token= await jwtTokenServices.GetToken(user,System.TimeSpan.FromMinutes(5))
                
                };
            }
            return null;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
        Token = await jwtTokenServices.GetToken(user, System.TimeSpan.FromMinutes(5))

            };
        }

        public async Task<UserDTO> Register(RegisterUserDTO registerUser, ModelStateDictionary modelState)
        {


            var user = new ApplicationUser()
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber,
               
            };

            var result = await userManager.CreateAsync(user, registerUser.Password);


            if (result.Succeeded)
            {
               await userManager.AddToRolesAsync(user, registerUser.Roles);

                return new UserDTO()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await jwtTokenServices.GetToken(user, System.TimeSpan.FromMinutes(5)),
                    Roles= await userManager.GetRolesAsync(user),
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(registerUser.Password) :
                               error.Code.Contains("Email") ? nameof(registerUser.Email) :
                               error.Code.Contains("Username") ? nameof(registerUser.Username) :
                               "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }


}
