using ImageProcessor;
using LibraryImageMerger.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace LibraryImageMerger.ImageMerging.Strategies
{
    public class MergeNone : IMergeStrategy
    {
        public MergedImageModel ImageModel { get; set; }

        public MemoryStream Execute()
        {
            MemoryStream outstream = new MemoryStream();

            // Creates a new image
            using Image bgImage = new Bitmap(ImageModel.Size.Width, ImageModel.Size.Height);
            using (Graphics graphics = Graphics.FromImage(bgImage))
            {
                graphics.FillRectangle(new SolidBrush(ImageModel.TintColor), 0, 0, ImageModel.Size.Width, ImageModel.Size.Height);
                graphics.Save();
            }

            using ImageFactory imageFactory = new ImageFactory();
            imageFactory
                .Load(bgImage)
                .Format(ImageModel.MediaFileFormat)
                .Save(outstream);
            return outstream;
        }
    }
}
