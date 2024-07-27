using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModel;

namespace ShoppingCart.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManager ;
		private SignInManager<AppUserModel> _signInManager;

		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl});
		}


		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, 
																											loginViewModel.Password, false, false);
				if (result.Succeeded)
				{
					return Redirect(loginViewModel.ReturnUrl??"/");
				}
				ModelState.AddModelError("", "Invalid username and password");
			}
			return View(loginViewModel);
		}

		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid)
			{
				AppUserModel newUser = new AppUserModel { UserName = user.UserName, Email = user.Email};
				IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
				if (result.Succeeded)
				{
					TempData["Succes"] = "Tạo thành công!";
					return RedirectToAction("Login", "Account");
				}
				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(user);
		}


		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();

			// Redirect to the specified URL or the default URL if returnUrl is not provided
			return Redirect(returnUrl);
		}


	}
}
