using System;
using System.IO;
using filecopier;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace runner
{
    class Program
    {
        static void Main(string[] args)
        {
             var builder = new ConfigurationBuilder()   
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
 
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            
            var startFolder = config["StartingFolder"];
            var publishFolderRoot = config["PublishFolder"];
            Console.WriteLine(startFolder);

            //need to look over the folders in the starting folder 

            var folders = Directory.GetDirectories(startFolder);
            foreach(var folder in folders) {
                var justFolderName = Path.GetFileName(folder);
                var publishFolder = Path.Combine(publishFolderRoot, justFolderName);
                //Console.WriteLine($"{folder}-{publishFolder}");
                if (!Directory.Exists(publishFolder)) {
                var exportFolder = Path.Combine(folder, "exported");
                    if (Directory.Exists(exportFolder)) {
                    //send it for processing
                    var copier = new Copier(publishFolderRoot);
                    copier.Process(exportFolder);
                    //Console.WriteLine("processing");
                    }
                }
            }


            Console.WriteLine(config["PublishFolder"]);
            //var copier = new Copier();
            //copier.Process("/Users/louise/Documents/Photos/20200403/export");
            Console.WriteLine("Hello World!");
        }
    }
}
