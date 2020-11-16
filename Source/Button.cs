using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace GraphFileManager
{
    public class Button
    {
        private Texture2D btn;
        private Rectangle btn_rec;

        private Texture2D icon;
        private Rectangle icon_rec;
        public Rectangle Rec { get{ return icon_rec; } }
        
        private string name;
        private Vector2 namePosition;
        private Vector2 nameOrigin;

        private int width = 90;
        private int padding = 30;

        public ICommand command;

        public Button(Vector2 pos, string type, string text, ICommand com=null)
        {
            name = text;
            btn = GraphFileManager.SContent.Load<Texture2D>("Textures/ButtonBG");
            btn_rec = new Rectangle((int)pos.X, (int)pos.Y, width, (int)((float)btn.Height/(float)btn.Width*(float)width));
            icon = GraphFileManager.SContent.Load<Texture2D>("Textures/"+type);
            icon_rec = new Rectangle((int)pos.X+padding/2, (int)pos.Y+padding/2, width-padding, (int)((float)icon.Height/(float)icon.Width*(float)(width-padding)));
            nameOrigin = new Vector2(GraphFileManager.Font.MeasureString(name).X / 2f, 0);
            namePosition = new Vector2(pos.X+icon_rec.Width/2f+padding/2, pos.Y+btn_rec.Height-padding/4-GraphFileManager.Font.MeasureString(name).Y);
            command = com != null ? com : new EmptyCommand();
        }

        public void SetCommand(ICommand com) {
            command = com;
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (btn_rec.Contains(mousePosition)) {
                if (mouseState.LeftButton == ButtonState.Pressed) {
                    command.Execute();
                }
            }
        }

        public void Draw()
        {
            GraphFileManager.SpriteBatchUI.Draw(btn, btn_rec, Color.White);
            GraphFileManager.SpriteBatchUI.Draw(icon, icon_rec, Color.White);
            GraphFileManager.SpriteBatchUI.DrawString(GraphFileManager.Font, name, namePosition, Color.Black,
                    0, nameOrigin, Vector2.One, SpriteEffects.None, 0);
        }
    }
}
