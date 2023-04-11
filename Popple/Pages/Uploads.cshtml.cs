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

namespace Popple.Pages
{
    public class UploadsModel : PageModel
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public UploadsModel(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            _bucketName = "popples3"; // Placeholder. Replace with your actual S3 bucket name - the username of a creator
            //_bucketName = _context.{Account instance}.Username
            
            /*HttpContext.Session.SetInt32("eId", employee[0].eId);
            HttpContext.Session.SetString("eName", employee[0].eName);
            HttpContext.Session.SetString("eUsername", employee[0].eUsername);
            HttpContext.Session.SetString("eRole", employee[0].eRole);*/ //For temporary session info
        }

        public List<string> Files { get; set; }
        
        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Files = await GetFilesAsync();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file != null && file.Length > 0 ) //Checks for creator Role if (&& _context.AccountsObject.Role == "Creator") added
            {
                var transferUtility = new TransferUtility(_s3Client);

                var key = Path.GetFileName(file.FileName);
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead // Make the uploaded file publicly accessible
                };

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
