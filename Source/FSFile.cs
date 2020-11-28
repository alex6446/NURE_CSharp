using System;
using System.IO;

namespace GraphFileManager
{

    public class FSFile : FSObject
    {

        public string Extension { 
            get {
                try {
                    return System.IO.Path.GetExtension(this.Path).Substring(1);
                } catch(System.ArgumentOutOfRangeException) {
                    return System.IO.Path.GetExtension(this.Path);
                }
            }
        }

        public FSFile(string p)
        {
            Path = p;
        }

    }

}


