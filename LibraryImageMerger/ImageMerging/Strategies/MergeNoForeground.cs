using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Filters.Photo;
using LibraryImageMerger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryImageMerger.ImageMerging.Strategies
{
    class MergeNoForeground : IMergeStrategy
    {
        public MergedImageModel ImageModel { get; set; }

        public MemoryStream Execute()
        {
            MemoryStream outstream = new MemoryStream();

            using ImageFactory imageFactory = new ImageFactory();
            imageFactory
                .Load(ImageModel.BackgroundFile)
                .Resize(new ResizeLayer(
                    size: ImageModel.Size,
                    resizeMode: ResizeMode.Crop)
                )
                .Filter(MatrixFilters.GreyScale)
                .Tint(ImageModel.TintColor)                
                .Format(ImageModel.MediaFileFormat)
                .Save(outstream); 

            return outstream;
        }
    }
}
