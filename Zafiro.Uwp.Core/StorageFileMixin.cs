﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace Zafiro.Uwp.Core
{
    public static class StorageFileMixin 
    {
        public static async Task<T> GetProperty<T>(this StorageFile storageFile, string name)
        {
            var dateEncodedPropertyName = name;
            var propertyNames = new List<string>
            {
                dateEncodedPropertyName
            };

            var extraProperties =
                await storageFile.Properties.RetrievePropertiesAsync(propertyNames);

            var propValue = (T) extraProperties[dateEncodedPropertyName];
            return propValue;
        }

        public static async Task<byte[]> GetThumbnailBytes(this StorageFile storageFile)
        {
            if (storageFile == null) throw new ArgumentNullException(nameof(storageFile));

            var storageItemThumbnail = await storageFile.GetThumbnailAsync(ThumbnailMode.VideosView);
            if (storageItemThumbnail == null) return null;

            return await storageItemThumbnail.AsStream().ToByteArray();
        }
    }
}