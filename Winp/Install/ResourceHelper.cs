using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cottle;
using Cottle.Values;

namespace Winp.Install
{
    internal static class ResourceHelper
    {
        private static readonly DocumentConfiguration Configuration = new DocumentConfiguration
        { BlockBegin = "{{", BlockContinue = "}|{", BlockEnd = "}}" };

        public static async Task<bool> WriteToFile<TOrigin>(string resourceName, IContext extraContext, string path)
        {
            var resourcePath = $"{typeof(TOrigin).Namespace}.{resourceName}";

            await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);

            if (stream == null)
                throw new ArgumentOutOfRangeException(nameof(resourceName), resourceName, @"invalid resource name");

            var directory = Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var templatePath = path + ".template";

            if (!File.Exists(templatePath))
            {
                using var template = File.Create(templatePath);

                await stream.CopyToAsync(template);
            }

            using var reader = new StreamReader(templatePath, Encoding.UTF8);
            await using var writer = new StreamWriter(File.Create(path));

            var documentResult = Document.CreateDefault(reader, Configuration);

            if (!documentResult.Success)
                return false;

            var context = Context.CreateCascade(extraContext, Context.CreateBuiltin(new Dictionary<Value, Value>
            {
                ["replace"] = new FunctionValue(Function.CreatePure(
                    (state, arguments) => arguments[0].AsString.Replace(arguments[1].AsString, arguments[2].AsString),
                    3)),
                ["trim"] = new FunctionValue(Function.CreatePure2((state, input, remove) =>
                    remove.AsString.Length > 0 ? input.AsString.Trim(remove.AsString[0]) : string.Empty))
            }));

            documentResult.Document.Render(context, writer);

            return true;
        }
    }
}