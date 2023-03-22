using System;
namespace Familynk.ViewModels
{
    public class ScrapBookVM
    {
        public ScrapBook Book { get; set; } = new();
        public FamilyUnit Family { get; set; } = default!;
        public Scrap NewScrap { get; set; } = new();
        public IFormFile? FileUpload { get; set; }
        public FileStream? NewFilePath { get; set; }
    }
}

