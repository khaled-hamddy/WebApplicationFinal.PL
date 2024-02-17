using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationFinal.DAL.Models;
using WebApplicationFinal.PL.Helpers;
using WebApplicationFinal.PL.ViewModels;

namespace WebApplicationFinal.PL.Controllers
{
	public class UserController: Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager ,IMapper mapper) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
        }

		public async  Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users =await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
					return View(users);
			}
			else
			{
				var user=await _userManager.FindByEmailAsync(email);
				var mappedUser =  new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel>() { mappedUser });
			}
		}
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null) // nullable has two properties hasvalue and value
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);


            return View(viewName, mappedUser);
        }
        public async Task<IActionResult> Edit(string id)
        {
            
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit([FromRoute]string id, UserViewModel updateduser)
        {
            if (id != updateduser.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.FName = updateduser.FName;
                    user.LName = updateduser.LName;
                    user.PhoneNumber = updateduser.PhoneNumber;
                    //user.Email = updateduser.Email;
                    //user.SecurityStamp=Guid.NewGuid().ToString();
                    await _userManager.UpdateAsync(user);
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Exception
                    //Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(updateduser);
        }
        public async Task<IActionResult> Delete(String id)
        {
            return await  Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> ConfirmDelete(string id)
        {
           

            //try and catch because it may throw exception if this Employee is inrelation in database and it is immutable
            try
            {

                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //Log Exception
                //Friendly Message
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
    }
}
