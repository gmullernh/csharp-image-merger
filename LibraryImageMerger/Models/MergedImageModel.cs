using ImageProcessor.Imaging.Formats;
using System.Drawing;

namespace LibraryImageMerger.Models
{
    public class MergedImageModel
    {
        public string BackgroundFile { get; set; }
        public string ForegroundFile { get; set; }

        public Size Size { get; set; }
        public Color TintColor { get; set; }

        public string OutputFile { get; set; }
        public ISupportedImageFormat MediaFileFormat {get;set;}
}
}
