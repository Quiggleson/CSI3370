using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Popple.Models;

namespace Popple.Pages
{
    public class SearchResultsModel : PageModel
    {
        private readonly PoppleContext _context;

        public SearchResultsModel(PoppleContext context)
        {
            _context = context;
        }

        public List<Comic> Comics { get; set; } = new List<Comic>();

        public List<Account> Accounts { get; set; } = new List<Account>();

        public void OnGet(string searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                Comics = _context.Comics.Where(c => c.ComicName.Contains(searchQuery)).ToList();

                Accounts = _context.Accounts
                    .Where(a => a.Username.Contains(searchQuery) || 
                                a.Comics.Any(c => c.ComicName.Contains(searchQuery)))
                    .ToList();
            }
        }
    }
}
