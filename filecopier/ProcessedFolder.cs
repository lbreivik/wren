using System;
using System.Collections.Generic;


namespace filecopier
{
    public class ProcessedFolder
    {
        public string Name {get;set;}

        private List<string> files = new List<string>();

        public List<string> Files {
            get {
            return files;
            }
        } 

        public void AddFile(string file) {
            files.Add(file);
        }
    }
}