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
    class MergeNoBackground : IMergeStrategy
    {
        public MergedImageModel ImageModel { get; set; }

        public MemoryStream Execute()
        {
            MemoryStream outstream = new MemoryStream();

            // Creates a solid background
            // https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-create-a-bitmap-at-run-time

            using Image bgImage = new Bitmap(ImageModel.Size.Width, ImageModel.Size.Height);
            using Brush b = new SolidBrush(ImageModel.TintColor);
            Graphics graphics = Graphics.FromImage(bgImage);
            graphics.FillRectangle(b, 0, 0, ImageModel.Size.Width, ImageModel.Size.Height);

            // Loads foreground image
            using ImageLayer fgImage = new ImageLayer
            {
                Image = Image.FromFile(ImageModel.ForegroundFile)
            };

            using ImageFactory imageFactory = new ImageFactory();
            imageFactory
                .Load(bgImage)
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
