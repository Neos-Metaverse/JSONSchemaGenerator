using FrooxEngine;
using NeosHeadless;
using NJsonSchema.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONSchemaGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new JsonSchemaGeneratorSettings();
            var generator = new JsonSchemaGenerator(settings);

            Type[] types = {  typeof(Config), typeof(NeosConfig) };

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            string outputPath = projectDirectory + "/output/";
            Directory.CreateDirectory(outputPath);

            foreach (Type type in types)
            {
                File.WriteAllText($"{outputPath}{type.Name}.schema.json", generator.Generate(type).ToJson());
            }
        }
    }
}
