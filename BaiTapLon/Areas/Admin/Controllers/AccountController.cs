using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaiTapLon.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using BaiTapLon.Models;

namespace BaiTapLon.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var UserWithRole = new List<UserWithRole>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserWithRole.Add(new UserWithRole { User = user, Role = roles.ToList() });
            }

            return View(UserWithRole);
        }
        public async Task<IActionResult> AssignRole(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            var UserRole = await _userManager.GetRolesAsync(user);
            var AllRole = await _roleManager.Roles.Select(r => new RoleVM { Id = r.Id, Name = r.Name }).ToListAsync();
            var ViewModel = new AssignRoleVM
            {
                UserId = UserId,
                AllRole = AllRole,
                SelectedRole = UserRole
            };
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                var userRole = await _userManager.GetRolesAsync(user);

                foreach (var role in model.SelectedRole)
                {
                    // Kiểm tra vai trò đã tồn tại trong hệ thống chưa
                    var roleExist = await _roleManager.RoleExistsAsync(role);
                    if (!roleExist)
                    {
                        ModelState.AddModelError(string.Empty, $"Role '{role}' does not exist.");
                        return View(model);
                    }

                    if (!userRole.Contains(role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                foreach (var role in userRole)
                {
                    if (!model.SelectedRole.Contains(role))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                }

                return RedirectToAction(nameof(Index), "Account");
            }

            return View(model);
        }




    }
}
