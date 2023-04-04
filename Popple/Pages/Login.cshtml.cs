/*using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using HelloWorld.Data;
using HelloWorld.Models;


namespace HelloWorld.Pages;
public class LoginModel : PageModel
{
    private readonly TimecardContext _context;
    public LoginModel(TimecardContext context)
    {
        _context = context;
        Console.Write("context exists");
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public void OnGet()
    {
        // Show the login form
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // IList to protect against accounts with duplicate emails
        IList<Employee> employee = await _context.Employee
            .Where(_ => _.eEmail.Equals(Email))
            .ToListAsync();

        // Authenticate the user
        if (employee.Count == 0 || Password == null)
            return RedirectToPage();
        if (Password.Equals(employee[0].ePassword))
        {
            HttpContext.Session.SetInt32("eId", employee[0].eId);
            HttpContext.Session.SetString("eName", employee[0].eName);
            // Redirect to the home page
            return RedirectToPage("timeentry");
        }
        else
        {
            // Authentication failed, show error message
            ModelState.AddModelError("", "Invalid email or password");
            return Page();
        }
    }

}
*/