using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Winp.Install;

internal static class Archive
{
    public static async Task<string?> DownloadAndExtract(Uri archiveSource, string archivePath, Uri destination)
    {
        using var client = new HttpClient();

        HttpResponseMessage response;

        try
        {
            response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, archiveSource)
            {
                Headers =
                {
                    UserAgent =
                    {
                        new ProductInfoHeaderValue("Mozilla", "5.0"),
                        new ProductInfoHeaderValue("(Windows NT 10.0; Win64; x64)"),
                        new ProductInfoHeaderValue("AppleWebKit", "537.36"),
                        new ProductInfoHeaderValue("(KHTML, like Gecko)"),
                        new ProductInfoHeaderValue("Chrome", "146.0.0.0"),
                        new ProductInfoHeaderValue("Safari", "537.36")
                    }
                }
            });
        }
        catch (HttpRequestException exception)
        {
            return exception.Message;
        }

        await using var stream = await response.Content.ReadAsStreamAsync();

        try
        {
            await using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

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

        return null;
    }
}