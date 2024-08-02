using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BITS.Areas.Identity.Data;
using BITS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BITS.Models;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Primitives;

namespace BITS.Controllers
{
    public class BITSUserController : Controller
    {
        private readonly UserContext _context;
        private UserManager<BITSUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public BITSUserController(UserContext context, UserManager<BITSUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: BITSUser
        public async Task<IActionResult> Index(string[] roles, string? search)
        {
            var raw = await _roleManager.Roles.ToListAsync();
            List<string> _roles = new();
            foreach(var i in raw)
            {
                _roles.Add(i.Name);
            }
            ViewBag.Types = _roles;

            // Search By Name
            if (search != null)
            {
                var temp = await _userManager.Users.Where(user => user.Name.Contains(search)).ToListAsync();
                return View(temp);
            }

            // Search By Genres
            if (roles.Length > 0)
            {
                HashSet<string> set = roles.ToHashSet();
                List<BITSUser> list = await _userManager.Users.Where(user => set.Contains(user.Roles)).ToListAsync();
                return View(list);
            }

            return View(await _context.BITSUser.ToListAsync());
        }

        // GET: BITSUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var BITSUser = await _context.BITSUser
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (BITSUser == null)
            {
                return NotFound();
            }

            return View(BITSUser);
        }

        // GET: BITSUser/Create
        public IActionResult Create()
        {
            ViewBag.AvailableRoles = new SelectList(SeedData.roleNames);
            return View();
        }

        // POST: BITSUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BITSUser BITSUser)
        {
            if (ModelState.IsValid)
            {
                BITSUser.UserName = BITSUser.Email;
                var user = await _userManager.CreateAsync(BITSUser, BITSUser.Password);
                if (user.Succeeded)
                {
                    return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
                }
            }
            ViewBag.AvailableRoles = new SelectList(SeedData.roleNames);
            ViewBag.UserRole = BITSUser.Roles;
            return View(BITSUser);
        }

        // GET: BITSUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var BITSUser = await _context.BITSUser.FindAsync(id);
            if (BITSUser == null)
            {
                return NotFound();
            }
            ViewBag.AvailableRoles = new SelectList(SeedData.roleNames);
            ViewBag.UserRole = BITSUser.Roles;
            return View(BITSUser);
        }

        // POST: BITSUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BITSUser BITSUser)
        {
            if (!id.Equals(BITSUser.Id))
            {
                return NotFound();
            }   

            if (!ModelState.IsValid)
            {
                ViewBag.AvailableRoles = new SelectList(SeedData.roleNames);
                ViewBag.UserRole = BITSUser.Roles;
                return View(BITSUser);
            }

            var user = await _userManager.FindByIdAsync(BITSUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = BITSUser.UserName;
            user.Name = BITSUser.Name;
            user.Roles = BITSUser.Roles;
            var currRoles = await _userManager.GetRolesAsync(user);
            var removeRoles = await _userManager.RemoveFromRolesAsync(user, currRoles);
            var userRoleResult = await _userManager.AddToRoleAsync(user, BITSUser.Roles);
            if (!userRoleResult.Succeeded)
            {
                Debug.WriteLine("Fail to add role for user " + user.Name);
            }
            Debug.WriteLine("Add a new role for user " + user.Name);
            if (BITSUser.Password != null)
            {
                var token  = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, BITSUser.Password);
                if (!result.Succeeded)
                {
                    string error_msg = "";
                    foreach (var i in result.Errors)
                        error_msg = i.Description + Environment.NewLine;
                    ModelState.AddModelError("CustomError", error_msg);

                    return View(BITSUser);
                }
            }
            if(user.Email != BITSUser.Email)
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, BITSUser.Email);
                var result = await _userManager.ChangeEmailAsync(user, BITSUser.Email, token);
                user.Email = BITSUser.Email;
                if (!result.Succeeded)
                {
                    string error_msg = "";
                    foreach (var i in result.Errors)
                        error_msg = i.Description + Environment.NewLine;
                    ModelState.AddModelError("CustomError", error_msg);

                    return View(BITSUser);
                }
                var username_result = await _userManager.SetUserNameAsync(user, BITSUser.Email);
                if (!username_result.Succeeded)
                {
                    string error_msg = "";
                    foreach (var i in username_result.Errors)
                        error_msg = i.Description + Environment.NewLine;
                    ModelState.AddModelError("CustomError", error_msg);

                    return View(BITSUser);
                }
            }
            try
            {
                var result = await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Reload the entity to get the latest data from the database
                _context.Entry(BITSUser).Reload();
                // Handle the concurrency conflict, e.g., show a message to the user
                ModelState.AddModelError("", "The record you attempted to edit was modified by another user.");

                return View(BITSUser);
            }
            return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
        }

        // GET: BITSUser/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var BITSUser = await _context.BITSUser.FindAsync(id);
            if (BITSUser != null)
            {
                _context.BITSUser.Remove(BITSUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
        }

        // POST: BITSUser/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFromIndex(string id)
        {
            var BITSUser = await _context.BITSUser.FindAsync(id);
            if (BITSUser != null)
            {
                _context.BITSUser.Remove(BITSUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
        }

        private bool BITSUserExists(string id)
        {
            return _context.BITSUser.Any(e => e.Id.Equals(id));
        }

        private async Task<IActionResult> AddOrEditRoles(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                Debug.WriteLine(id + " does not exist.");
                return NotFound();
            }
            
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    //string error_msg = "";
                    //foreach (var i in roleResult.Errors)
                    //    error_msg = i.Description + Environment.NewLine;
                    //ModelState.AddModelError("CustomError", error_msg);
                    Debug.WriteLine(roleName + " fail to create");
                    return NotFound();
                }
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(roleName))
            {
                Debug.WriteLine(user.Name + " already has the role");
                return NotFound();
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                //string error_msg = "";
                //foreach (var i in result.Errors)
                //    error_msg = i.Description + Environment.NewLine;
                //ModelState.AddModelError("CustomError", error_msg);
                Debug.WriteLine(user.Name + " fail to add role");
                return NotFound();
            }
            return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
        }

        //public async Task<IActionResult> UserEdit(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return NotFound();
        //    }

        //    var BITSUser = await _context.BITSUser.FindAsync(id);
        //    if (BITSUser == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(BITSUser);
        //}

        // POST: BITSUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

    }

}
