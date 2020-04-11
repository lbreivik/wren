using System;
using System.IO;

namespace filecopier
{
    public class Copier
    {

        private string ProcessedFolder;

        public Copier(string processedFolder) {
            this.ProcessedFolder = processedFolder;
        }
        public void Process(string path) {
            string[] files = 
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            var processedFolder = new ProcessedFolder();  
            foreach(var f in files) {
                processedFolder.AddFile(f);
                var rawFileInfo = getMatchingRawFile(f);
                if (string.IsNullOrEmpty(processedFolder.Name)) {
                    processedFolder.Name = rawFileInfo.ParentFolderName;
                }               
                processedFolder.AddFile(rawFileInfo.RawFilePath);

                Console.WriteLine(f);
                Console.WriteLine($"{getMatchingRawFile(f).ParentFolderName}-{getMatchingRawFile(f).RawFilePath}" );
            }  
            CopyFiles(processedFolder);  
        }

        private RawFileInfo getMatchingRawFile(string jpg) {
            var result = Path.GetFileNameWithoutExtension(jpg);
            var parentFolder = Directory.GetParent(Directory.GetParent(jpg).FullName);
            var parentRawFile = Path.Combine(parentFolder.FullName, result + ".CR2");
            return new RawFileInfo() { ParentFolderName = parentFolder.Name, RawFilePath = parentRawFile};
        }

        private void CopyFiles(ProcessedFolder pf) {
            var destFolder = Path.Combine(ProcessedFolder, pf.Name);
            if (!Directory.Exists(destFolder)) {
                Directory.CreateDirectory(destFolder);
            }
            foreach(var f in pf.Files) {
                if (File.Exists(f)) {
                    var destFilePath = Path.Combine(destFolder, Path.GetFileName(f));
                    File.Copy(f, destFilePath);
                }
                else {
                    Console.WriteLine($"couldn't find the file {f}");
                }

            }

        }
    }
}
