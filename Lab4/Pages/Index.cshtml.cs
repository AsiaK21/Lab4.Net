using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System;

namespace Lab4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<string> Images { get; set; }
        IWebHostEnvironment _environment;
        public string imagesDir { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment)
        {
            _environment = environment;
            imagesDir = Path.Combine(_environment.WebRootPath, "images");

        }

        public void OnGet()
        {
            UpdateFileList();
        }
        private void UpdateFileList()
        {
            Images = new List<string>();
            foreach (var item in Directory.EnumerateFiles(imagesDir).ToList())
            {
                Images.Add(Path.GetFileName(item));
            }
        }
    }
}