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

namespace BITS.Controllers
{
    public class BITSUserController : Controller
    {
        private readonly UserContext _context;
        private UserManager<BITSUser> _userManager;
        public BITSUserController(UserContext context, UserManager<BITSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BITSUser
        public async Task<IActionResult> Index(string[] types, string? search)
        {
            // Search By Name
            if (search != null)
            {
                var temp = await _userManager.Users.Where(user => user.Email.Contains(search)).ToListAsync();
                return View(temp);
            }

            // Search By Genres
            if (types.Length > 0)
            {
                HashSet<string> set = types.ToHashSet();
                List<BITSUser> list = await _userManager.Users.ToListAsync();
                return View(list.Where(user => set.Contains(user.Roles)));
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
            return View();
        }

        // POST: BITSUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,Name,Roles,Salt,HashedPassword,PhoneNumber,StreetAddress,PostCode,Suburb,State,CardNumber,CardOwner,Expiry,CVV")] BITSUser BITSUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(BITSUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
            }
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
                return View(BITSUser);
            }

            var user = await _userManager.FindByIdAsync(BITSUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = BITSUser.UserName;
            user.Name = BITSUser.Name;
            user.Email = BITSUser.Email;

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
    }
}
