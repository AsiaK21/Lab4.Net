using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ImageMagick;

namespace Lab4.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile Upload { get; set; }
        private readonly IWebHostEnvironment _environment;
        private string imagesDir;

        public UploadModel(IWebHostEnvironment environment)
        {
            _environment = environment;
            imagesDir = Path.Combine(_environment.WebRootPath, "images");
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (Upload != null)
            {
                string extension = ".jpg";
                switch (Upload.ContentType)
                {
                    case "image/png":
                        extension = ".png";
                        break;
                    case "image/gif":
                        extension = ".gif";
                        break;
                }
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + extension;
                var filePath = Path.Combine(imagesDir, fileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await Upload.CopyToAsync(fs);
                }

                using (var image = new MagickImage(filePath))
                using (var watermark = new MagickImage(Path.Combine(_environment.WebRootPath, "watermark.png")))
                {

                    watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 4);

                    image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);
                    image.Write(filePath);
                }

            }

            return RedirectToPage("Index");
        }
    }
}
