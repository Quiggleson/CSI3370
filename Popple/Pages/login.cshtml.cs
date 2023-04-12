using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
//using HelloWorld.Data;
using Popple.Models;

namespace Popple.Pages;

//namespace HelloWorld.Pages;
public class LoginModel : PageModel
{
    
    private readonly Popple.Models.PoppleContext _context;
    public LoginModel(Popple.Models.PoppleContext context)
    {
        _context = context;
        Console.Write("context exists");
    }
    
    
    [BindProperty]
    public string Email { get; set; }
    //private readonly ILogger<LoginModel> _logger;

    [BindProperty]
    public string Password { get; set; }
    /*
    public void OnGet()
    public LoginModel(ILogger<LoginModel> logger)
    {
        // Show the login form
        _logger = logger;
    }
    */
    public async Task<IActionResult> OnPostAsync()
    {
        // IList to protect against accounts with duplicate emails
        IList<Account> Account = await _context.Accounts
            .Where(_ => _.Email.Equals(Email))
            .ToListAsync();

        // Authenticate the user
        if (Account.Count == 0 || Password == null)
            return RedirectToPage();
        if (Password.Equals(Account[0].Password))
        {
            HttpContext.Session.SetInt32("AccountId", Account[0].AccountId);
            HttpContext.Session.SetString("Username", Account[0].Username);
            HttpContext.Session.SetString("Role", Account[0].Role);
            // Redirect to the home page
            return RedirectToPage("/Index");
        }
        else
        {
            // Authentication failed, show error message
            ModelState.AddModelError("", "Invalid email or password");
            return RedirectToPage();
        }
    }

}
