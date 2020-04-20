using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Filters.Photo;
using LibraryImageMerger.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace LibraryImageMerger.ImageMerging.Strategies
{
    public class MergeBoth : IMergeStrategy
    {
        public MergedImageModel ImageModel { get; set; }

        public MemoryStream Execute()
        {
            MemoryStream outstream = new MemoryStream();

            using ImageLayer fgImage = new ImageLayer
            {
                Image = Image.FromFile(ImageModel.ForegroundFile)
            };

            using ImageFactory imageFactory = new ImageFactory();
            imageFactory
                .Load(ImageModel.BackgroundFile)
                .Resize(new ResizeLayer(
                size: ImageModel.Size,
                resizeMode: ResizeMode.Crop)
                )
                .Filter(MatrixFilters.GreyScale)
                .Tint(ImageModel.TintColor)
                .Overlay(fgImage)
                .Format(ImageModel.MediaFileFormat)
                .Save(outstream);

            return outstream;
        }
    }
}
