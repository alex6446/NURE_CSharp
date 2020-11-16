using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace GraphFileManager
{

    public interface IGraphComponent
    {
        Vector2 Position {get; set;}
        public void Update();
        public void DrawText();
        public void DrawIcon();
    }

}
