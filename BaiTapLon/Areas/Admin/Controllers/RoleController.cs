using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using BaiTapLon.Models;

namespace HMK_PROJECT.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    var role = new AppRole { Name = Name };

                    await _roleManager.CreateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(Name);

        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id, string Name)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = Name;
            await _roleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string Id)
        {
            var Role = await _roleManager.FindByIdAsync(Id);
            if (Role != null)
            {
                await _roleManager.DeleteAsync(Role);
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
