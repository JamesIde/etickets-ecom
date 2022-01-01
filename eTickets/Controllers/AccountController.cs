using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }


        public IActionResult Login()
        {
            var loginVM = new LoginVM();
           
            
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
           
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Invalid Credentials. Please try again";
                return View(loginVM);
            }

            TempData["Error"] = "Invalid Credentials. Please try again";    
            return View(loginVM);
        }

        public IActionResult Signup()
        {
            var signupVM = new SignupVM();
            return View(signupVM);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupVM signupVM)
        {
            if (!ModelState.IsValid) return View(signupVM);

            var user = await _userManager.FindByEmailAsync(signupVM.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(signupVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = signupVM.Name,
                Email = signupVM.Email,
                UserName = signupVM.Email
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, signupVM.Password);

            if (!newUserResponse.Succeeded)
            {
                TempData["Error"] = "Please ensure your password has a capital letter, number and non-alphabetic symbol";
                return View(signupVM);
            }
            else
            {
               await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("SignupSuccess");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Movies");
        }

        public async Task<IActionResult> ListUsers()
        {
            var users = await _db.Users.ToListAsync();
            return View(users);
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
