using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace GraphFileManager {

    public class GraphComposite : IGraphComponent {

        private IGraphComponent[] nodes;

        private FSFolder fso; 
        public Vector2 Position {get; set;}

        private Texture2D icon;
        private Vector2 textOrigin;
        private Vector2 iconOrigin;
        private Color lineColor;
        private Color iconColor;

        private int depth;
        private double angle;

        public GraphComposite(FSFolder fsobj, Vector2 pos, int d, double a)
        {
            fso = fsobj;
            Position = pos;
            depth = d;
            angle = a;
            textOrigin = new Vector2(GraphFileManager.Font.MeasureString(fso.Name).X / 2f, 0);
            lineColor = Color.Gray;
            iconColor = depth == 0 ? Color.Blue : Color.White;
            Console.WriteLine(fso.Path);
            icon = GraphFileManager.SContent.Load<Texture2D>("Textures/folder");
            textOrigin.Y = -icon.Height/2f*0.08f;
            iconOrigin = new Vector2(icon.Width * 0.5f, icon.Height * 0.5f);
            if (depth < GraphFileManager.MaxDepth && fso != null)
                GenerateNodes();
        }

        private void GenerateNodes()
        {
            string[] files = ((FSFolder)fso).GetFiles();
            string[] folders = ((FSFolder)fso).GetFolders();
            int N = files.Length + folders.Length;
            int L = (GraphFileManager.MaxDepth - depth) * 100;
            double step = 2 * Math.PI / (depth == 0 ? N : N + 1);
            if (N == 0)
                return;
            nodes = new IGraphComponent[N];
            int index = 0;
            for (int i = 0; i < files.Length; ++i, ++index) {
                Vector2 pos = (new Vector2((float)Math.Cos(step*(index+1) + angle), (float)Math.Sin(step*(index+1) + angle))) * 200 + Position;
                nodes[index] = new GraphLeaf(new FSFile(files[i]), pos);
            }
            for (int i = 0; i < folders.Length; ++i, ++index) {
                Vector2 pos = (new Vector2((float)Math.Cos(step*(index+1) + angle), (float)Math.Sin(step*(index+1) + angle))) * L + Position;
                nodes[index] = new GraphComposite(new FSFolder(folders[i]), pos,
                        depth+1, Math.Atan2(pos.Y-Position.Y, pos.X-Position.X) + Math.PI);
            }
            
        }

        public void Update()
        {
            if (nodes != null)
                foreach (var node in nodes)
                    node.Update();
        }

        public void DrawLines()
        {
            if (nodes != null)
                foreach (var node in nodes) {
                        DrawLine(Position, node.Position, lineColor);
                    if (node is GraphComposite) {
                        ((GraphComposite)node).DrawLines();
                    }
                }
        }
        
        public void DrawIcon()
        {
            GraphFileManager.SpriteBatchGraph.Draw(icon, Position, null, iconColor, -GraphFileManager.SCamera.Rotation, 
                    new Vector2(icon.Width * 0.5f, icon.Height * 0.5f), new Vector2(0.08f, 0.08f), SpriteEffects.None, 0f);
            if (nodes != null)
                foreach (var node in nodes)
                    node.DrawIcon();
        }

        public void DrawText()
        {
            GraphFileManager.SpriteBatchGraph.DrawString(GraphFileManager.Font, fso.Name, Position, Color.Red,
                    -GraphFileManager.SCamera.Rotation, textOrigin, Vector2.One, SpriteEffects.None, 0);
            if (nodes != null)
                foreach (var node in nodes)
                    node.DrawText();
        }

        // Legacy code
        
        private static Texture2D _texture;
        private static Texture2D GetTexture()
        {
            SpriteBatch spriteBatch = GraphFileManager.SpriteBatchGraph;
            if (_texture == null)
            {
                _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _texture.SetData(new[] {Color.White});
            }

            return _texture;
        }

        public static void DrawLine(Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            SpriteBatch spriteBatch = GraphFileManager.SpriteBatchGraph;
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(point1, distance, angle, color, thickness);
        }

        public static void DrawLine(Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            SpriteBatch spriteBatch = GraphFileManager.SpriteBatchGraph;
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(GetTexture(), point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

    }

}
