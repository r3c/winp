using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace Winp.Install
{
    internal static class ArchiveHelper
    {
        private const string UserAgent =
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.112 Safari/535.1";

        public static async Task<string?> DownloadAndExtract(Uri archiveSource, string archivePath, Uri destination)
        {
            using (var client = new WebClient())
            {
                byte[] data;

                client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);

                try
                {
                    data = await client.DownloadDataTaskAsync(archiveSource);
                }
                catch (WebException exception)
                {
                    return exception.Message;
                }

                await using var stream = new MemoryStream(data);

                try
                {
                    using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

                    var prefix = string.IsNullOrEmpty(archivePath) ? string.Empty : archivePath.TrimEnd('/') + "/";

                    foreach (var entry in archive.Entries)
                    {
                        if (entry.FullName.Length <= prefix.Length || !entry.FullName.StartsWith(prefix))
                            continue;

                        var sourcePathRelative = entry.FullName.Substring(prefix.Length);
                        var destinationPath = Path.Combine(destination.AbsolutePath, sourcePathRelative);

                        if (entry.Length == 0)
                            Directory.CreateDirectory(destinationPath);
                        else
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath) ?? string.Empty);

                            entry.ExtractToFile(destinationPath, true);
                        }
                    }
                }
                catch (InvalidDataException exception)
                {
                    return exception.Message;
                }
                catch (IOException exception)
                {
                    return exception.Message;
                }
            }

            return null;
        }
    }
}