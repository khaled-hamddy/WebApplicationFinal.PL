using AutoMapper;
using DEMO.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationFinal.DAL.Models;
using WebApplicationFinal.PL.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplicationFinal.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                if(role is not null)
                {
                    var mappedRole = new RoleViewModel()
                    {
                        Id = role.Id,
                        RoleName = role.Name

                    };
                    return View(new List<RoleViewModel>() { mappedRole });
                }return View (Enumerable.Empty<RoleViewModel>());
              
            }
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null) // nullable has two properties hasvalue and value
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);


            return View(viewName, mappedRole);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                var mappedRole=_mapper.Map<RoleViewModel,IdentityRole>(roleVM);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(roleVM);
        }
        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel updatedRole)
        {
            if (id != updatedRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = updatedRole.RoleName;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Exception
                    //Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(updatedRole);
        }
        public async Task<IActionResult> Delete(String id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {


            //try and catch because it may throw exception if this Employee is inrelation in database and it is immutable
            try
            {

                var user = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(user);
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
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return BadRequest();
            ViewBag.RoleId = roleId;
            var usersInRole = new List<UsersInRoleViewModel>();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var userInRole = new UsersInRoleViewModel
                {
                    UserName = user.UserName,
                    UserId = user.Id
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UsersInRoleViewModel> users, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return BadRequest();
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser != null)
                    {
                        if (user.IsSelected && !(await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && (await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);

                    }
                }
                return RedirectToAction(nameof(Edit), new { id = roleId });
            }
            return View(users);
        }
    } 
}
