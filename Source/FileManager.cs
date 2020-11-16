using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GraphFileManager {

    public class FileManager {

        public GraphComposite Root {get; set;}
        public string Dir {get; private set;}


        public FileManager(string dir)
        {
            Dir = dir;
            Root = new GraphComposite(new FSFolder(dir), new Vector2(0, 0), 0, 0);
        }

        public void Update()
        {
            Root.Update();
        }

        public void Draw()
        {
            Root.DrawLines();
            Root.DrawIcon();
            Root.DrawText();
        }

    }

}
