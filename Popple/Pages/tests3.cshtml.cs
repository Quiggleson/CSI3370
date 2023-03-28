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

namespace Popple.Pages
{
    public class tests3Model : PageModel
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public tests3Model(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            _bucketName = "popples3"; // Replace with your actual S3 bucket name
        }

        public List<string> Files { get; set; }

        public async Task OnGetAsync()
        {
            Files = await GetFilesAsync();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
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

                return RedirectToPage("./tests3");
            }

            return Page();
        }

        private async Task<List<string>> GetFilesAsync()
        {
            var response = await _s3Client.ListObjectsAsync(_bucketName);
            var files = response.S3Objects.Select(x => x.Key).ToList();
            return files;
        }
    }
}
