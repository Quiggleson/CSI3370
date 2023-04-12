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
        private readonly Popple.Models.PoppleContext _context;

        public FavoritesModel(Popple.Models.PoppleContext context)
        {
            _context = context;
            Console.WriteLine("Context exists");
        }

        [BindProperty]
        public Favorite Favorite { get;set; } = default!;

        [BindProperty]
        public Account Account { get;set; } = default!;

        [BindProperty]
        public Comic Comic { get;set; } = default!;

        public IList<Favorite> FavoritesList { get;set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Favorites == null || Favorite == null) {
                return Page();
            }

            int sessionAccountId = (int) HttpContext.Session.GetInt32("AccountId");
            Favorite newFavorite = new Favorite();
            Favorite.AccountId = sessionAccountId;
            //Favorite.ComicName is dealt with in the backend.
            Account ?userAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == sessionAccountId);
            Comic ?newFavoriteComic = _context.Comics.FirstOrDefault(a => a.ComicName == Favorite.ComicName);
            Favorite.Account = userAccount;
            Favorite.ComicNameNavigation = newFavoriteComic;
            _context.Favorites.Add(Favorite);
            await _context.SaveChangesAsync();
            
            //Gets user account and comic objects from database for the Favorite object's Account and 
            //ComicNameNavigation properties
            /*if (userAccount != null && newFavoriteComic != null) {  
                Favorite.Account = userAccount;
                Favorite.ComicNameNavigation = newFavoriteComic;
                _context.Favorites.Add(Favorite);
                await _context.SaveChangesAsync();
            } else {
                RedirectToPage("./Favorites");
            }*/
            
            return RedirectToPage("./Favorites");
        }
        
        public async Task OnGetAsync()
        {     
            if (_context.Favorites != null) {
                //Front end attempt to get a personalized list.
                //int sessionAccountId = (int) HttpContext.Session.GetInt32("AccountId"); 
                //FavoritesList = await _context.Favorites.Where(Favorite, Favorite.AccountId == sessionAccountId).ToListAsync();
                FavoritesList = await _context.Favorites.ToListAsync();
            }
        }
    }
}
