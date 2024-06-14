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
        public async Task<IActionResult> Index()
        {
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
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,Name,Roles,Salt,HashedPassword,PhoneNumber,StreetAddress,PostCode,Suburb,State,CardNumber,CardOwner,Expiry,CVV")] BITSUser BITSUser)
        {
            if (!id.Equals(BITSUser.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(BITSUser.Id);
                    user.Name = BITSUser.Name;
                    user.UserName = BITSUser.UserName;
                    user.Salt = BITSUser.Salt;
                    var result = await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BITSUserExists(BITSUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Reload the entity to get the latest data from the database
                        _context.Entry(BITSUser).Reload();

                        // Handle the concurrency conflict, e.g., show a message to the user
                        ModelState.AddModelError("", "The record you attempted to edit was modified by another user.");
                        return View(BITSUser);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(BITSUser);
        }

        // GET: BITSUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
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

        // POST: BITSUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var BITSUser = await _context.BITSUser.FindAsync(id);
            if (BITSUser != null)
            {
                _context.BITSUser.Remove(BITSUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BITSUserExists(string id)
        {
            return _context.BITSUser.Any(e => e.Id.Equals(id));
        }
    }
}
