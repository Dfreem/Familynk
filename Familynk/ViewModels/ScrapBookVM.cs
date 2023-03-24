using System;
namespace Familynk.ViewModels
{
    public class ScrapBookVM
    {
        public ScrapBook Book { get; set; } = new();
        public FamilyUnit Family { get; set; } = default!;
        public Scrap NewScrap { get; set; } = new();
        public List<IFormFile> FileUpload { get; set; } = new();
        List<byte[]> Images { get; set; } = new();
        [DisplayName("New Comment")]
        public string? NewComment { get; set; } = "";
    }
}

