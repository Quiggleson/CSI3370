using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Popple.Pages;

public class SearchResultsModel : PageModel
{
    private readonly ILogger<SearchResultsModel> _logger;

    public SearchResultsModel(ILogger<SearchResultsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
/*
protected void Page_Load(object sender, EventArgs e)
{
    if (!string.IsNullOrEmpty(Request.QueryString["searchQuery"]))
    {
        string query = Request.QueryString["searchQuery"];
        // Perform search logic using the query value
    }
}
*/