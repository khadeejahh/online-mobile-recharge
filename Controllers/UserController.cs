using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mobile_Phone_Project.Areas.Identity.Data;

namespace Mobile_Phone_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;

        public UserController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
    }
}
