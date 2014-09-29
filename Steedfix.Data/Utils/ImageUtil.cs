using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Steedfix.Domain;

namespace Steedfix.Data.Utils
{
    public class ImageUtil
    {
        private const string ImageRoot = @"C:\Users\rweltz\Google Drive\MSc\7WCM0035\Code\Steedfix\Steedfix\Steedfix.Data\SeedImages\Small";

        /// <summary>
        /// Create an image object using a FileInfo object
        /// </summary>
        /// <param name="file"></param>
        /// <param name="createdByUserId">Who created the file</param>
        /// <returns></returns>
        private Image GetImage(FileInfo file, string createdByUserId)
        {
            var imageFile = System.Drawing.Image.FromFile(string.Format(@"{0}\{1}", ImageRoot, file.Name));
            byte[] imageFileBytes;
            using (var memoryStream = new MemoryStream())
            {
                imageFile.Save(memoryStream,System.Drawing.Imaging.ImageFormat.Jpeg);
                imageFileBytes = memoryStream.ToArray();
            }

            var title = file.Name.Replace("-", " ").Replace(".jpg", "");

            return new Image()
            {
                ContentType = "image/jpeg",
                Title = title,
                FileName = file.Name,
                ImageBytes = imageFileBytes,
                CreatedByUserId = createdByUserId
            };
        }

        /// <summary>
        /// Add a bunch of image records to the images table using data in the seed images folder
        /// </summary>
        /// <param name="context">The db context</param>
        /// <param name="createdbyUserId">The admin user who creates the images</param>
        public void SeedImages(SteedfixContext context, string createdbyUserId)
        {
            var folder = new DirectoryInfo(ImageRoot);

            var files = folder.GetFiles();
            
            foreach (var fileInfo in files)
            {
                var image = GetImage(fileInfo, createdbyUserId);
                context.Images.AddOrUpdate(i => i.FileName, image);
            }
        }
    }
}
