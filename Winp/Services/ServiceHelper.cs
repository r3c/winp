using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cottle;
using Cottle.Values;

namespace Winp.Services
{
    internal static class ServiceHelper
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

        public static async Task<string?> WriteConfiguration(string resourceName, string path, IContext extraContext)
        {
            await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            if (stream == null)
                return "missing resource";

            using var reader = new StreamReader(stream, Encoding.UTF8);
            await using var writer = new StreamWriter(File.Create(path));

            var documentConfiguration = new DocumentConfiguration
                {BlockBegin = "{{", BlockContinue = "}|{", BlockEnd = "}}"};
            var documentResult = Document.CreateDefault(reader, documentConfiguration);

            if (!documentResult.Success)
                return "invalid template";

            var context = Context.CreateCascade(extraContext, Context.CreateBuiltin(new Dictionary<Value, Value>
            {
                ["replace"] = new FunctionValue(Function.CreatePure(
                    (state, arguments) => arguments[0].AsString.Replace(arguments[1].AsString, arguments[2].AsString),
                    3)),
                ["trim"] = new FunctionValue(Function.CreatePure2((state, input, remove) =>
                    remove.AsString.Length > 0 ? input.AsString.Trim(remove.AsString[0]) : string.Empty))
            }));

            documentResult.Document.Render(context, writer);

            return null;
        }
    }
}