using System;
using System.IO;

namespace GraphFileManager
{

    public class FSObject
    {
        public string Path { get; protected set; }
        public string Name { 
            get {
                return System.IO.Path.GetFileName(Path);
            }
        }
        
    }

}
