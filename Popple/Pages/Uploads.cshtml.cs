using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Popple.Models;
using Microsoft.EntityFrameworkCore;

namespace Popple.Pages
{
    public class UploadsModel : PageModel
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly Popple.Models.PoppleContext _context;

        public UploadsModel(IAmazonS3 s3Client, Popple.Models.PoppleContext context)
        {
            _s3Client = s3Client;
            _bucketName = "popples3"; 
            _context = context;
            Console.Write("context exists");
        }

        public List<string> Files { get; set; }
        
        [BindProperty]
        public Account Account { get; set; } = default!;

        [BindProperty]
        public Comic Comic { get; set; }

        public async Task OnGetAsync()
        {
            Files = await GetFilesAsync();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file != null && file.Length > 0 && HttpContext.Session.GetString("Role").Equals("Creator")) //Checks for creator Role if (&& _context.AccountsObject.Role == "Creator") added
            //Why doesn't ToLowerCase() work here?
            {
                var transferUtility = new TransferUtility(_s3Client); 
                String key = "" + HttpContext.Session.GetString("Username") + "/" + Comic.ComicName;

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead // Make the uploaded file publicly accessible
                };

                // IList to protect against comics with duplicate titles
                IList<Comic> Comics = await _context.Comics
                    .Where(_ => _.ComicName.Equals(Comic.ComicName))
                    .ToListAsync();

                //Assigning Comic post data members not already assigned via identical HTML backend naming:
                Comic.PostDate = DateTime.Now;
                Comic.CreatorId = (int) HttpContext.Session.GetInt32("AccountId");
                _context.Add(Comic);
                await _context.SaveChangesAsync();
                
                await transferUtility.UploadAsync(fileTransferUtilityRequest);

                return RedirectToPage("./Uploads");
            } 

            return Page();
        }

        private async Task<List<string>> GetFilesAsync()
        {
            var response = await _s3Client.ListObjectsAsync(_bucketName); //_bucketName to be later replaced by _context.{Account instance}.Username
            var files = response.S3Objects.Select(x => x.Key).ToList();
            return files;
        }
    }
}
