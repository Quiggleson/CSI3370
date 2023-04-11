using Popple.Models;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Popple.Pages;
public class ComicModel : PageModel
{

    private readonly PoppleContext _context;
    private readonly IAmazonS3 _client;

    public string link { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ComicModel(PoppleContext context, IAmazonS3 client)
    {
        _client = client;
        _context = context;
    }
    public static string GetContents(string path)
    {
        HttpWebRequest request = HttpWebRequest.Create(path) as HttpWebRequest;
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
    public async Task<IActionResult> OnGetAsync(string username, string comicId)
    {
        Username = username;
        string ComicName = comicId.Split('.')[0];
        IList<Comic> comics = await _context.Comics
            .Where(c => c.Name.Equals(ComicName))
            .ToListAsync();
        if (comics == null)
        {
            Console.WriteLine("oi it's gone!");
            return NotFound();
        }

        Name = comics[0].Name;
        Description = comics[0].Description;

        GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
        {
            BucketName = "popples3",
            Key = $"{username}/{ComicName}.png",
            Expires = DateTime.UtcNow.AddMinutes(5)
        };

        link = await Task.Run(() => _client.GetPreSignedURL(request));


        return Page();
    }

}
