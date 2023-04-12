using Popple.Models;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Popple.Pages
{
    public class CreatorModel : PageModel
    {

        private readonly PoppleContext _context;
        private readonly IAmazonS3 _client;

        public string Username { get; set; }
        public IList<Comic> Comics { get; set; }

        public CreatorModel(PoppleContext context, IAmazonS3 client)
        {
            _client = client;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string username)
        {
            Username = username;
            IList<Account> accounts = await _context.Accounts
                .Where(c => c.Username.Equals(Username))
                .ToListAsync();

            if (accounts.Count < 0){
                Console.WriteLine("No creators found");
                return Page();
            }
            int AccountId = accounts[0].AccountId;

            Comics = await _context.Comics
                .Where(c => c.CreatorId.Equals(AccountId))
                .ToListAsync();

            return Page();
        }
    }
}
