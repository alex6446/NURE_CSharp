using System;

namespace GraphFileManager
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var fm = GraphFileManager.Open())
                fm.Run();
        }
    }
}
