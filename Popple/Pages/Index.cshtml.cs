using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Popple.Models;
using Microsoft.EntityFrameworkCore;

namespace Popple.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Popple.Models.PoppleContext _context;

    public IndexModel(ILogger<IndexModel> logger, Popple.Models.PoppleContext context)
    {
        _logger = logger;
        _context = context;
        Console.Write("context exists");
    }

    [BindProperty]
    public Account Account { get; set; } = default!;

    public void OnGet()
    {

    }
}
