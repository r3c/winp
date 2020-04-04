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
        public static async Task<bool> WriteToFile(string resourceName, IContext extraContext, string path)
        {
            await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            if (stream == null)
                throw new ArgumentOutOfRangeException(nameof(resourceName), resourceName, @"invalid resource name");

            using var reader = new StreamReader(stream, Encoding.UTF8);
            await using var writer = new StreamWriter(File.Create(path));

            var documentConfiguration = new DocumentConfiguration
                {BlockBegin = "{{", BlockContinue = "}|{", BlockEnd = "}}"};
            var documentResult = Document.CreateDefault(reader, documentConfiguration);

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