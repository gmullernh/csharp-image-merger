using LibraryImageMerger.Models;
using System.IO;

namespace LibraryImageMerger.ImageMerging
{
    /// <summary>
    /// The interface for merging images.
    /// </summary>
    public interface IMergeStrategy
    {
        public MergedImageModel ImageModel { get; set; }

        public MemoryStream Execute();
    }
}
