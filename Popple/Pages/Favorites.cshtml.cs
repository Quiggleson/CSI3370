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

namespace Popple.Pages
{
    public class FavoritesModel : PageModel
    {
        private readonly PoppleContext _context;

        public FavoritesModel(PoppleContext context)
        {
            _context = context;
        }

        public IList<Favorite> FavoritesList { get; set; } = default!;
        public IList<Account> AccountsList { get; set; }

        public async Task OnGetAsync()
        {
            int? accountId = HttpContext.Session.GetInt32("AccountId");
            if (accountId != null)
            {
                var favoritesQuery = _context.Favorites
                    .Where(f => f.AccountId == accountId.Value)
                    .Include(f => f.ComicNameNavigation);
                var accountsQuery = _context.Accounts.AsQueryable();
                var result = await favoritesQuery.Join(
                    _context.Comics,
                    f => f.ComicName,
                    c => c.ComicName,
                    (f, c) => new { Favorite = f, Comic = c }
                ).Join(
                    accountsQuery,
                    fc => fc.Comic.CreatorId,
                    a => a.AccountId,
                    (fc, a) => new { Favorite = fc.Favorite, Account = a }
                ).ToListAsync();
                var favoritesWithCreator = result.Select(
                    r => new { r.Favorite.ComicName, r.Account.Username }
                );
                ViewData["FavoritesWithCreator"] = favoritesWithCreator;
            }
        }

    }
}