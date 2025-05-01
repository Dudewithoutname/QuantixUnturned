using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Qnx.Items.Generator;

[Generator]
public class BuffGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var buffType = context.CompilationProvider
            .Select((compilation, _) => compilation.GetTypeByMetadataName("Qnx.Core.Enums.EBuff"));

        context.RegisterSourceOutput(buffType, (spc, ebuffSymbol) =>
        {
            if (ebuffSymbol == null) return;

            var sourceBuilder = new StringBuilder(@"
                using Qnx.Core.Enums;
                using Qnx.Items.Models;
                namespace Qnx.Items.Models.Buffs.Generated
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
                        public sealed record {className} : Buff
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