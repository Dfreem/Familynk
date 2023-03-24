using System;
namespace Familynk.ViewModels
{
    public class ScrapBookVM
    {
        public ScrapBook Book { get; set; } = new();
        public FamilyUnit Family { get; set; } = default!;
        public Scrap Edit { get; set; } = new();
        public IFormFile FileUpload { get; set; } = default!;
        List<byte[]> Images { get; set; } = new();
        [DisplayName("New Comment")]
        public string? NewComment { get; set; } = "";
    }
}

