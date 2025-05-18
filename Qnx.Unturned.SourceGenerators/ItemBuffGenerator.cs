using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Qnx.Unturned.SourceGenerators;

[Generator]
public class ItemBuffGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var buffType = context.CompilationProvider
            .Select((compilation, _) => compilation.GetTypeByMetadataName("Qnx.Core.Enums.EBuff"));

        context.RegisterSourceOutput(buffType, (spc, ebuffSymbol) =>
        {
            if (ebuffSymbol == null) return;

            var sourceBuilder = new StringBuilder(@"
                using Qnx.Unturned.Buffs;
                using Qnx.Unturned.Items.Models;
                namespace Qnx.Unturned.Models.Buffs.Generated
                {
            ");

            foreach (var member in ebuffSymbol
                         .GetMembers()
                         .OfType<IFieldSymbol>()
                         .Where(m => m.ConstantValue != null))
            {
                var name = member.Name;
                var className = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name.ToLower());

                sourceBuilder.AppendLine(
                    $@"
                        public sealed record {className} : ItemBuff
                        {{
                            public override EBuff Origin => EBuff.{name};
                        }}
                    "
                );
            }

            sourceBuilder.Append("}");
            spc.AddSource("Buffs_Generated.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        });
    }
}