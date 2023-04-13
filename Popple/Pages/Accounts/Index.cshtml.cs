using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Popple.Models;

namespace Popple.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly Popple.Models.PoppleContext _context;

        public IndexModel(Popple.Models.PoppleContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {        
            if (_context.Accounts != null && (HttpContext.Session.GetInt32("AccountId") != null))
            {
                Account = await _context.Accounts
                .Where(_ => _.AccountId == HttpContext.Session.GetInt32("AccountId")) 
                .ToListAsync();
            }
        }
    }
}
