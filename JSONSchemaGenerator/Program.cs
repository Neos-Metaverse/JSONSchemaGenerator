using FrooxEngine;
using NeosHeadless;
using NJsonSchema.Generation;
using System;
using System.IO;
using System.Text.Json;

namespace JSONSchemaGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            // Ok we so we have a mixed use of Newtonsoft and System.Text.Json so we need to use both for now. I don't like this but hey, I'm working on it ;)

            string outputPath = projectDirectory + "/output/";
            Directory.CreateDirectory(outputPath);

            Type[] systemJsonTypes = { typeof(Config) };
            var systemJsonSettings = new JsonSchemaGeneratorSettings
            {
                SerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            };

            var systemGenerator = new JsonSchemaGenerator(systemJsonSettings);

            foreach (Type type in systemJsonTypes)
            {
                File.WriteAllText($"{outputPath}{type.Name}.schema.json", systemGenerator.Generate(type).ToJson());
            }

            var newtonJsonSettings = new JsonSchemaGeneratorSettings();
            var newtonGenerator = new JsonSchemaGenerator(newtonJsonSettings);

            Type[] newtonJsonTypes = { typeof(NeosConfig) };

            foreach (Type type in newtonJsonTypes)
            {
                File.WriteAllText($"{outputPath}{type.Name}.schema.json", newtonGenerator.Generate(type).ToJson());
            }
        }
    }
}
