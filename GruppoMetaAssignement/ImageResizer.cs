using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose;
using Aspose.Imaging;

namespace GruppoMetaAssignement;

internal class ImageResizer
{
    private readonly XmlParser _config;

    public ImageResizer(XmlParser config)
    {
        _config = config;
    }

    public string Resize(string sizeName, string filePath)
    {
        var archivePath = _config.Get("archive");
        var cachePath = _config.Get("cache");
        var width = _config.Get($"{sizeName}/width");
        var height = _config.Get($"{sizeName}/height");
        var isCrop = _config.Get($"{sizeName}/crop");
        var filters = _config.Get($"{sizeName}/filters");

        //var cacheDir = Path.Combine((string)cachePath, sizeName);
        //var cacheFile = Path.Combine(cacheDir, fileName);



        //if (!Directory.Exists(cacheDir))
        //{
        //    Directory.CreateDirectory(cacheDir);
        //}

        //if (!File.Exists(filePath))
        //{
        //    throw new ArgumentException($"File non trovato: {filePath}");
        //}

        //if (File.Exists(cacheFile))
        //{
        //    var originalLastWriteTimeUtc = File.GetLastWriteTimeUtc(filePath);
        //    var cachedLastWriteTimeUtc = File.GetLastWriteTimeUtc(cacheFile);

        //    if (originalLastWriteTimeUtc <= cachedLastWriteTimeUtc)
        //    {
        //        return cacheFile;
        //    }
        //}

        using (Image image = Image.Load(filePath))
        {
            
            if (height != "*" && width != "*")
            {
                image.Resize((int)width, (int)height);
            }
            else if (height == "*")
            {
                // var newHeight = (int)(image.Width * ((double)height / image.Height));
                image.Resize((int)width, Image.GetProportionalHeight(image.Width, image.Height, (int)width));
            }
            else if (width == "*")
            {
                image.Resize(Image.GetProportionalWidth(image.Width, image.Height, (int)height), (int)height);
            }
            else
            {
                throw new ArgumentException($"Fornire almeno una tra width o height");
            }

            ApplyFilters();
            image.Save(@"C:\Users\franc\Source\Repos\Francesco-Fera\GruppoMetaAssignement\GruppoMetaAssignement\imageCache");
            return "";
        }
    }

    private static void ApplyFilters()
    {
        // Se presenti, applica i filtri
    }

}
