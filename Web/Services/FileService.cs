using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Win32;

 

namespace Web.Services
{
    public class FileService : IFileService
    {

        private readonly string _placeToStore = $"{HttpRuntime.AppDomainAppPath}/Content/TempFiles";
        private const string VirtualFileStorage = "/Content/ApplicationFiles";
        private const string DefaultProfileImgRelativeUrl = "/Content/DefaultProfileImages/";
        private readonly string _defaultProfileImageUrl;

        public FileService()
        {
            var deployedLocation = GetDepolyedLocation();
            _defaultProfileImageUrl = $"{deployedLocation}{DefaultProfileImgRelativeUrl}";
        }
        public void DeleteFileIfExists(string uri)
        {
            // ReSharper disable once RedundantJumpStatement
            if (uri == null) return;
            var fileName = uri.Substring(uri.LastIndexOf("/", StringComparison.Ordinal) + 1);
            var pathToDelete = $"{_placeToStore}/{fileName}";
            if (File.Exists(pathToDelete))
            {
                File.Delete(Path.Combine(pathToDelete));
            }
        }

        public string SaveFileAsync(Stream stream, string fileName)
        {
            var generatedName = GenerateFileName(fileName);
            var placeToStore = $"{_placeToStore}/{generatedName}";
            SaveStream(stream, placeToStore);
            return $"{_placeToStore}/{generatedName}";
        }

        public string GeneratePath(string str1, string str2)
        {
            return Path.Combine(str1, str2);
        }

        public string GenerateFileName(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            return $"{Guid.NewGuid()}{fileExtension}";
        }

        public async Task<string> SaveFileAsync(Uri uri)
        {
            try
            {
                return await DownoadFile(uri, _placeToStore);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<string> DownoadFile(Uri fileUri, string locationToStoreTo)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(fileUri))
            {
                response.EnsureSuccessStatusCode();
                var mediaType = response.Content.Headers.ContentType.MediaType;
                var fileName = $"fileName.{ExtractExtension(mediaType)}";
                var stream = await response.Content.ReadAsStreamAsync();
                return SaveFileAsync(stream, fileName);
            }
        }

        private static string ExtractExtension(string mediaType)
        {
            var key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mediaType, false);
            var value = key?.GetValue("Extension", null);
            var result = value?.ToString() ?? string.Empty;
            return result;
        }

        public static void SaveStream(Stream stream, string placeToStore)
        {
            using (var fileStream = new FileStream(placeToStore, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }
        }

        public string GenerateDefaultProfile()
        {


            var randNumber = new Random().Next(10);
            var imageUrl = DefaultProfileImgRelativeUrl + "default_profile_" + randNumber + ".jpg";
            return imageUrl;
        }

        public string GetDepolyedLocation()
        {
            return HttpRuntime.AppDomainAppPath;
        }

        private async Task<IEnumerable<string>> GetListOfFiles(string directoryPath)
        {
            return await Task.Factory.StartNew(() =>
                       Directory.EnumerateFiles(directoryPath)
                    );
        }

        public void DeleteFromTempFiles(string uri)
        {
            if (File.Exists(uri))
            {
                File.Delete(uri);
            }
        }

    }


    public interface IFileService
    {
        Task<string> SaveFileAsync(Uri uri);
        void DeleteFileIfExists(string uri);
        string SaveFileAsync(Stream stream, string fileName);
        string GeneratePath(string str1, string str2);
        string GenerateFileName(string fileName);
        string GenerateDefaultProfile();

        void DeleteFromTempFiles(string uri);
        string GetDepolyedLocation();
    }
}