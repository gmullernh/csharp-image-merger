using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace LibraryImageMerger
{
    public class ImageMerger
    {
        /// <summary>
        /// Defines the merge mode to be used
        /// </summary>
        private enum MergeMode
        {
            NoBackground,
            NoForeground,
            Both,
            None
        }

        Models.MergedImageModel MergedImageModel { get; set; }
        ImageMerging.IMergeStrategy MergeStrategy { get; set; }

        public ImageMerger(string outputFile, Size size, Color color, string backgroundFile, string foregroundFile)
        {
            MergedImageModel = new Models.MergedImageModel()
            {
                BackgroundFile = backgroundFile,
                ForegroundFile = foregroundFile,
                OutputFile = outputFile,
                MediaFileFormat = new JpegFormat(),
                Size = size,
                TintColor = color
            };

            SaveImageToFile(Execute(SetExportMode()), MergedImageModel.OutputFile);
        }

        /// <summary>
        /// Defines the export mode based on the enum MergeMode and the images supplied
        /// </summary>
        private MergeMode SetExportMode()
        {
            // If the background is null, but the foreground not.
            if (string.IsNullOrEmpty(MergedImageModel.BackgroundFile) 
                && !string.IsNullOrEmpty(MergedImageModel.ForegroundFile))
                return MergeMode.NoBackground;
            
            // If the foreground is null, but the backgroud not.
            if (!string.IsNullOrEmpty(MergedImageModel.BackgroundFile) && string.IsNullOrEmpty(MergedImageModel.ForegroundFile))
                return MergeMode.NoForeground;
            
            // If both aren't null, else, both are null.
            if (!string.IsNullOrEmpty(MergedImageModel.BackgroundFile) && !string.IsNullOrEmpty(MergedImageModel.ForegroundFile))
                return MergeMode.Both;
            
            return MergeMode.None;
        }


        /// <summary>
        /// Executes the logic for creating the new image
        /// </summary>
        private MemoryStream Execute(MergeMode mode)
        {
            switch (mode)
            {
                case MergeMode.Both:
                    MergeStrategy = new ImageMerging.Strategies.MergeBoth();
                    break;
                case MergeMode.NoBackground:
                    MergeStrategy = new ImageMerging.Strategies.MergeNoBackground();
                    break;
                case MergeMode.NoForeground:
                    MergeStrategy = new ImageMerging.Strategies.MergeNoForeground();
                    break;
                default:    // None
                    MergeStrategy = new ImageMerging.Strategies.MergeNone();
                    break;
            }

            MergeStrategy.ImageModel = MergedImageModel;
            return MergeStrategy.Execute();
        }

        private void SaveImageToFile(MemoryStream ms, string fullFileName)
        {
            // Creates the folder path if not exists.
            CreatePathIfNotExists(fullFileName.Split('/')[^1]);

            // Saves the file
            using (FileStream fs = new FileStream(fullFileName, FileMode.Create, FileAccess.Write))
                ms.CopyTo(fs);

            Console.WriteLine($"File saved at {fullFileName}");
        }

        private void CreatePathIfNotExists(string path)
        {
            // Creates a path if it doesn't exist in the system
            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);
        }
    }
}
