using System;
using System.IO;

namespace filecopier
{
    public class Copier
    {

        public void Process(string path) {
            string[] files = 
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach(var f in files) {
                Console.WriteLine(f);
                Console.WriteLine($"{getMatchingRawFile(f).ParentFolderName}-{getMatchingRawFile(f).RawFilePath}" );
            }    
        }

        private RawFileInfo getMatchingRawFile(string jpg) {
            var result = Path.GetFileNameWithoutExtension(jpg);
            var parentFolder = Directory.GetParent(Directory.GetParent(jpg).FullName);
            var parentRawFile = Path.Combine(parentFolder.FullName, result + ".CR2");
            return new RawFileInfo() { ParentFolderName = parentFolder.Name, RawFilePath = parentRawFile};
        }
    }
}
