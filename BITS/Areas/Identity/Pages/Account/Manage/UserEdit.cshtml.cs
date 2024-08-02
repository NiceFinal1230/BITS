// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BITS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BITS.Areas.Identity.Pages.Account.Manage
{
    public class UserEditModel : PageModel
    {
        private readonly UserManager<BITSUser> _userManager;
        private readonly SignInManager<BITSUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public UserEditModel( UserManager<BITSUser> userManager, SignInManager<BITSUser> signInManager, ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            public string StreetAddress { get; set; } = string.Empty;
            public string Suburb { get; set; } = string.Empty;
            public string PostCode { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
        }
        public string StreetAddress { get; set; } = string.Empty;
        public string Suburb { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            StreetAddress = user.StreetAddress;
            Suburb = user.Suburb;
            PostCode = user.PostCode;
            State = user.State;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Retrieve the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if(Input.StreetAddress == null || Input.PostCode == null || Input.State == "")
            {
                ModelState.AddModelError(string.Empty, "You are required to fill in the Street Address, PostCode and State.");
                return Page();
            }


            // Validate the street address input
            if (Regex.IsMatch(Input.StreetAddress, @"^(?=.*[a-zA-Z])(?=.*\d).+$"))
            {
                user.StreetAddress = Input.StreetAddress;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Street address is not valid. It must contains character and alpha.");
                return Page();
            }

            // Validate the postcode input
            int value;
            bool isNumber = int.TryParse(Input.PostCode, out value);
            if (isNumber == true)
            {
                user.PostCode = Input.PostCode;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Postcode can only contains number!");
                return Page();
            }

            // Define a set of valid states
            HashSet<string> states = new HashSet<string> { "Australian Capital Territory", "New South Wales", "Northern Territory", "Queensland", "South Australia", "Tasmania", "Victoria", "Western Australia" };

            if (states.Contains(Input.State))
            {
                user.State = Input.State;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "You must choose one state.");
                return Page();
            }

            // Update the suburb
            user.Suburb = Input.Suburb;

            // Update the user's profile in the database
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return Page();
                }
            }

            // Set the status message
            StatusMessage = "Your profile has been changed.";

            // Redirect to the same page to reflect changes
            return RedirectToPage();
        }
    }
}
