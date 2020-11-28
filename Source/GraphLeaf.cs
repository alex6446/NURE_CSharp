using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace GraphFileManager {

    public class GraphLeaf : IGraphComponent {

        private FSFile fso; 

        private Texture2D icon;
        public Vector2 Position {get; set;}
        private Vector2 textOrigin;
        private Vector2 iconOrigin;
        private Color lineColor;
        private Color iconColor;
        private float scale;

        private static Dictionary<string, string> ficons;

        static GraphLeaf()
        {
            ficons = new Dictionary<string, string>();

            ficons.Add("word", "doc docx");
            ficons.Add("csharp", "cs");
            ficons.Add("picture", "bmp ico img jpg png");
        }


        public GraphLeaf(FSFile fsobj, Vector2 pos)
        {
            fso = fsobj;
            Position = pos;
            textOrigin = new Vector2(GraphFileManager.Font.MeasureString(fso.Name).X / 2f, 0);
            lineColor = Color.Gray;
            iconColor = Color.White;
            icon = GraphFileManager.SContent.Load<Texture2D>("Textures/" + this.GetIconName());
            scale = 26.44f / icon.Height;
            textOrigin.Y = -icon.Height/2f*scale;
            iconOrigin = new Vector2(icon.Width * 0.5f, icon.Height * 0.5f);
        }

        public void Update()
        {
        }

        public void DrawIcon()
        {
            GraphFileManager.SpriteBatchGraph.Draw(icon, Position, null, iconColor, -GraphFileManager.SCamera.Rotation, 
                    new Vector2(icon.Width * 0.5f, icon.Height * 0.5f), new Vector2(scale, scale), SpriteEffects.None, 0f);
        }

        public void DrawText()
        {
            GraphFileManager.SpriteBatchGraph.DrawString(GraphFileManager.Font, fso.Name, Position, Color.Red,
                    -GraphFileManager.SCamera.Rotation, textOrigin, Vector2.One, SpriteEffects.None, 0);
        }

        public string GetIconName()
        {
            string ext = fso.Extension;
            if (ext == "") return "file";
            foreach (var kvp in ficons)
                if (kvp.Value.Contains(ext))
                    return kvp.Key;
            return "file";
        }
        
    }

}

