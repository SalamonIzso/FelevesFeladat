using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.Service
{
    public class MediaService : IMediaService
    {
        public async Task<string> CapturePhotoAsync()
        {
            if (!MediaPicker.Default.IsCaptureSupported) return null;
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            return await SaveToCache(photo);
        }

        public async Task<string> PickPhotoAsync()
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();
            return await SaveToCache(photo);
        }

        private async Task<string> SaveToCache(FileResult photo)
        {
            if (photo == null) return null;
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFilePath);
            await sourceStream.CopyToAsync(localFileStream);
            return localFilePath;
        }
    }
}
