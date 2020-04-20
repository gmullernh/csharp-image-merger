using System;
using System.Drawing;

namespace ConsoleImageMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating images.");
            Random rnd = new Random();

            string outputPath = $"{Environment.CurrentDirectory}/Result/{DateTime.Now.ToString("yyyyMMdd_hhmmss")}_{rnd.Next()}.jpg";
            string backgroundPath = $"{Environment.CurrentDirectory}/Source/Background/view-of-sunset-behind-windshield.jpg";
            string foregroundPath = $"{Environment.CurrentDirectory}/Source/Foreground/sun.png";

            // Uses both images
            new LibraryImageMerger.ImageMerger(
                outputPath, 
                new Size(1024, 1024), 
                Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), 
                backgroundPath, 
                foregroundPath);

            outputPath = $"{Environment.CurrentDirectory}/Result/{DateTime.Now.ToString("yyyyMMdd_hhmmss")}_{rnd.Next()}.jpg";

            // Uses background image
            new LibraryImageMerger.ImageMerger(
                outputPath,
                new Size(1024, 1024),
                Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                null,
                foregroundPath);

            outputPath = $"{Environment.CurrentDirectory}/Result/{DateTime.Now.ToString("yyyyMMdd_hhmmss")}_{rnd.Next()}.jpg";

            // Uses foreground image
            new LibraryImageMerger.ImageMerger(
                outputPath,
                new Size(1024, 1024),
                Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                backgroundPath,
                null);

            outputPath = $"{Environment.CurrentDirectory}/Result/{DateTime.Now.ToString("yyyyMMdd_hhmmss")}_{rnd.Next()}.jpg";

            // None
            new LibraryImageMerger.ImageMerger(
                outputPath,
                new Size(1024, 1024),
                Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                null,
                null);
        }
    }
}
