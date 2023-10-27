using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Lab4.Pages;

public class SingleModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Image { get; set; }

    private readonly IWebHostEnvironment _environment;

    public SingleModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public IActionResult OnGet()
    {
        var imagesDir = Path.Combine(_environment.WebRootPath, "images");
        if (System.IO.File.Exists(Path.Combine(imagesDir, Image)))
        {
            return Page();
        }
        else
        {
            return NotFound();
        }
    }

}
