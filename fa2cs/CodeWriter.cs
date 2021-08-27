using System;
using System.Collections.Generic;
using System.Text;
using fa2cs.Helpers;

namespace fa2cs
{
    public class CodeWriter
    {
        const string indent = "    ";

        public string Write(IEnumerable<FontAwesomeIcon> icons)
        {
            Console.Write("Generating C# code...");

            var classTemplate = ResourcesHelper.ReadResourceContent("ClassTemplate.txt");
            var propertyTemplate = ResourcesHelper.ReadResourceContent("PropertyTemplate.txt");

            var properties = new List<string>();
            
            var enumerables = new StringBuilder();

            foreach (var icon in icons)
            {
                var property = propertyTemplate.Replace("$link$", icon.Url)
                                       .Replace("$name$", icon.Name)
                                       .Replace("$code$", icon.Unicode)
                                       .Replace("$secondaryCode$", icon.SecondaryUnicode)
                                       .Replace("$dotnet_name$", icon.DotNetName)
                                       .Replace("$styles$", icon.StylesSummary);

                properties.Add(property);

                enumerables.AppendLine($"\t\t\t\t_all.Add({icon.DotNetName}.FaName, {icon.DotNetName});");
            }

            var separator = Environment.NewLine + Environment.NewLine;
            
            var code = string.Join(separator, properties);

            classTemplate = classTemplate.Replace("$enumerable$", enumerables.ToString());

            return classTemplate.Replace("$properties$", code);
        }
    }
}
