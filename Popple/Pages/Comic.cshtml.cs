using Popple.Models;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Popple.Pages
{
    public class ComicModel : PageModel
    {

        private readonly PoppleContext _context;
        private readonly IAmazonS3 _client;

        public string link { get; set; }
        public string Username { get; set; }
        public string ComicName { get; set; }
        public string Description { get; set; }
        public ComicModel(PoppleContext context, IAmazonS3 client)
        {
            _client = client;
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string username, string comicName)
        {
            Username = username;
            ComicName = comicName.Split('.')[0];
            IList<Comic> comics = await _context.Comics
                .Where(c => c.ComicName.Equals(ComicName))
                .ToListAsync();
            if (comics == null || comics.Count == 0)
            {
                Console.WriteLine("oi it's gone!");
                return NotFound();
            }

            Description = comics[0].ComicDescription;

            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = "popples3",
                Key = $"{username}/{ComicName}",
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

            link = await Task.Run(() => _client.GetPreSignedURL(request));
            if (HttpContext.Session.GetInt32("AccountId") != null)
            {
                int accountId = (int) HttpContext.Session.GetInt32("AccountId");
                Favorite favorite = await _context.Favorites
                    .FirstOrDefaultAsync(f => f.AccountId == accountId && f.ComicName == ComicName);

                if (favorite != null)
                {
                    ViewData["IsFavorite"] = true;
                }
                else
                {
                    ViewData["IsFavorite"] = false;
                }
            }
            return Page();
        }


        public async Task<IActionResult> OnPostFavoriteAsync()
        {
            Favorite favorite = new Favorite
            {
                AccountId = (int)HttpContext.Session.GetInt32("AccountId"),
                ComicName = Request.Form["comicName"]
            };

            _context.Add(favorite);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUnfavoriteAsync()
        {
            Favorite favorite = new Favorite
            {
                AccountId = (int)HttpContext.Session.GetInt32("AccountId"),
                ComicName = Request.Form["comicName"]
            };

            _context.Remove(favorite);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

    }
}