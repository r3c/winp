using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cottle;

namespace Winp.Install;

internal static class Template
{
    private static readonly DocumentConfiguration Configuration = new()
    {
        BlockBegin = "{{",
        BlockContinue = "}|{",
        BlockEnd = "}}"
    };

    public static async Task<bool> WriteToFile<TOrigin>(string resourceName, IContext extraContext, string path)
    {
        var resourcePath = $"{typeof(TOrigin).Namespace}.{resourceName}";

        await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);

        if (stream == null)
            throw new ArgumentOutOfRangeException(nameof(resourceName), resourceName, @"invalid resource name");

        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        // Constant `Builtin.CompileTime` is generated before build, see project file
        var compileDateTime = new DateTime(Builtin.CompileTicks, DateTimeKind.Local);
        var templatePath = path + ".template";

        if (!File.Exists(templatePath) || File.GetLastWriteTime(templatePath) < compileDateTime)
        {
            await using var template = File.Create(templatePath);

            await stream.CopyToAsync(template);
        }

        using var reader = new StreamReader(templatePath, Encoding.UTF8);
        await using var writer = new StreamWriter(File.Create(path));

        var documentResult = Document.CreateDefault(reader, Configuration);

        if (!documentResult.Success)
            return false;

        var context = Context.CreateCascade(extraContext, Context.CreateBuiltin(new Dictionary<Value, Value>
        {
            ["replace"] = Value.FromFunction(Function.CreatePure3((_, source, from, to) =>
                source.AsString.Replace(from.AsString, to.AsString))),
            ["trim"] = Value.FromFunction(Function.CreatePure2((_, input, remove) =>
                remove.AsString.Length > 0 ? input.AsString.Trim(remove.AsString[0]) : string.Empty))
        }));

        documentResult.Document.Render(context, writer);

        return true;
    }
}