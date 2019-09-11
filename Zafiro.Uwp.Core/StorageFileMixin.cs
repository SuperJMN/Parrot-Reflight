using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace Zafiro.Uwp.Core
{


    public static class StorageFileMixin
    {
        static string[] props = new[] {
    "System.AcquisitionID									",
    "System.ApplicationDefinedProperties					",
    "System.ApplicationName									",
    "System.AppZoneIdentifier								",
    "System.Author											",
    "System.CachedFileUpdaterContentIdForConflictResolution	",
    "System.CachedFileUpdaterContentIdForStream				",
    "System.Capacity										",
    "System.Category										",
    "System.Comment											",
    "System.Company											",
    "System.ComputerName									",
    "System.ContainedItems									",
    "System.ContentStatus									",
    "System.ContentType										",
    "System.Copyright										",
    "System.CreatorAppId									",
    "System.CreatorOpenWithUIOptions						",
    "System.DataObjectFormat								",
    "System.DateAccessed									",
    "System.DateAcquired									",
    "System.DateArchived									",
    "System.DateCompleted									",
    "System.DateCreated										",
    "System.DateImported									",
    "System.DateModified									",
    "System.DefaultSaveLocationDisplay						",
    "System.DueDate											",
    "System.EndDate											",
    "System.ExpandoProperties								",
    "System.FileAllocationSize								",
    "System.FileAttributes									",
    "System.FileCount										",
    "System.FileDescription									",
    "System.FileExtension									",
    "System.FileFRN											",
    "System.FileName										",
    "System.FileOfflineAvailabilityStatus					",
    "System.FileOwner										",
    "System.FilePlaceholderStatus							",
    "System.FileVersion										",
    "System.FindData										",
    "System.FlagColor										",
    "System.FlagColorText									",
    "System.FlagStatus										",
    "System.FlagStatusText									",
    "System.FolderKind										",
    "System.FolderNameDisplay								",
    "System.FreeSpace										",
    "System.FullText										",
    "System.HighKeywords									",
    "System.ImageParsingName								",
    "System.Importance										",
    "System.ImportanceText									",
    "System.IsAttachment									",
    "System.IsDefaultNonOwnerSaveLocation					",
    "System.IsDefaultSaveLocation							",
    "System.IsDeleted										",
    "System.IsEncrypted										",
    "System.IsFlagged										",
    "System.IsFlaggedComplete								",
    "System.IsIncomplete									",
    "System.IsLocationSupported								",
    "System.IsPinnedToNameSpaceTree							",
    "System.IsRead											",
    "System.IsSearchOnlyItem								",
    "System.IsSendToTarget									",
    "System.IsShared										",
    "System.ItemAuthors										",
    "System.ItemClassType									",
    "System.ItemDate										",
    "System.ItemFolderNameDisplay							",
    "System.ItemFolderPathDisplay							",
    "System.ItemFolderPathDisplayNarrow						",
    "System.ItemName										",
    "System.ItemNameDisplay									",
    "System.ItemNameDisplayWithoutExtension					",
    "System.ItemNamePrefix									",
    "System.ItemNameSortOverride							",
    "System.ItemParticipants								",
    "System.ItemPathDisplay									",
    "System.ItemPathDisplayNarrow							",
    "System.ItemSubType										",
    "System.ItemType										",
    "System.ItemTypeText									",
    "System.ItemUrl											",
    "System.Keywords										",
    "System.Kind											",
    "System.KindText										",
    "System.Language										",
    "System.LastSyncError									",
    "System.LastWriterPackageFamilyName						",
    "System.LowKeywords										",
    "System.MediumKeywords									",
    "System.MileageInformation								",
    "System.MIMEType										",
    "System.Null											",
    "System.OfflineAvailability								",
    "System.OfflineStatus									",
    "System.OriginalFileName								",
    "System.OwnerSID										",
    "System.ParentalRating									",
    "System.ParentalRatingReason							",
    "System.ParentalRatingsOrganization						",
    "System.ParsingBindContext								",
    "System.ParsingName										",
    "System.ParsingPath										",
    "System.PerceivedType									",
    "System.PercentFull										",
    "System.Priority										",
    "System.PriorityText									",
    "System.Project											",
    "System.ProviderItemID									",
    "System.Rating											",
    "System.RatingText										",
    "System.RemoteConflictingFile							",
    "System.Sensitivity										",
    "System.SensitivityText									",
    "System.SFGAOFlags										",
    "System.SharedWith										",
    "System.ShareUserRating									",
    "System.SharingStatus									",
    "System.Shell.OmitFromView								",
    "System.SimpleRating									",
    "System.Size											",
    "System.SoftwareUsed									",
    "System.SourceItem										",
    "System.SourcePackageFamilyName							",
    "System.StartDate										",
    "System.Status											",
    "System.StorageProviderCallerVersionInformation			",
    "System.StorageProviderError							",
    "System.StorageProviderFileChecksum						",
    "System.StorageProviderFileIdentifier					",
    "System.StorageProviderFileRemoteUri					",
    "System.StorageProviderFileVersion						",
    "System.StorageProviderFileVersionWaterline				",
    "System.StorageProviderId								",
    "System.StorageProviderShareStatuses					",
    "System.StorageProviderSharingStatus					",
    "System.StorageProviderStatus							",
    "System.Subject											",
    "System.SyncTransferStatus								",
    "System.Thumbnail										",
    "System.ThumbnailCacheId								",
    "System.ThumbnailStream									",
    "System.Title											",
    "System.TitleSortOverride								",
    "System.TotalFileSize									",
    "System.Trademarks										",
    "System.TransferOrder									",
    "System.TransferPosition								",
    "System.TransferSize									",
    "System.VolumeId										",
    "System.ZoneIdentifier									"
};

        public static async Task<T> GetProperty<T>(this StorageFile storageFile, string name)
        {
            var dateEncodedPropertyName = name;
            var propertyNames = new List<string>
            {
                dateEncodedPropertyName
            };

            var extraProperties =
                await storageFile.Properties.RetrievePropertiesAsync(propertyNames);

            var propValue = (T)extraProperties[dateEncodedPropertyName];
            return propValue;
        }

        public static async Task<byte[]> GetThumbnailBytes(this StorageFile storageFile)
        {
            if (storageFile == null) throw new ArgumentNullException(nameof(storageFile));

            var storageItemThumbnail = await storageFile.GetThumbnailAsync(ThumbnailMode.VideosView);
            if (storageItemThumbnail == null) return null;

            return await storageItemThumbnail.AsStream().ToByteArray();
        }

        public static async Task<(string, object)[]> GetAllProperties(this StorageFile file)
        {
            var allProperties = props.Select(async name =>
            {
                try
                {
                    name = name.TrimEnd();
                    var property = await file.GetProperty<object>(name);
                    return (name, property);
                }
                catch (Exception e)
                {
                    return (name, "{Failed}");
                }
            });

            return await Task.WhenAll(allProperties);
        }
    }
}