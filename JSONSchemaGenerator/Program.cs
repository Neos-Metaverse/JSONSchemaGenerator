using FrooxEngine;
using NeosHeadless;
using NJsonSchema.Generation;
using System;
using System.IO;

namespace JSONSchemaGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonSchemaGeneratorSettings settings = new JsonSchemaGeneratorSettings();
            settings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;

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
