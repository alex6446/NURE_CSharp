using System;
using System.IO;

namespace GraphFileManager
{

    public class FSFolder : FSObject
    {

        public FSFolder(string p)
        {
            Path = p;
        }

        public string[] GetFiles()
        {
            return Directory.GetFiles(Path);
        }
        
        public string[] GetFolders()
        {
            return Directory.GetDirectories(Path);
        }
        
    }

}

