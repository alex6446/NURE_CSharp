using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GraphFileManager
{
    public class GraphFileManager : Game
    {
        private static GraphFileManager instance;

        private GraphicsDeviceManager graphics;
        public static SpriteBatch SpriteBatchGraph { get; private set; }
        public static SpriteBatch SpriteBatchUI { get; private set; }

        public static ContentManager SContent {get; private set;}
        public static SpriteFont Font {get; private set;}

        public static Camera SCamera;
        public FileManager Filemanager {get; set;}
        public static int MaxDepth = 20;

        public static Button[] Buttons;

        public static GraphFileManager Open(string dir=".")
        {
            if (instance == null)
                instance = new GraphFileManager(dir);
            return instance;
        }

        private GraphFileManager(string dir)
        {
            SContent = Content;
            SContent.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            Font = SContent.Load<SpriteFont>("MyFont");
            Filemanager = new FileManager(dir);
        }

        protected override void Initialize()
        {
            Buttons = new Button[2];
            Buttons[0] = new Button(new Vector2(0, 0), "close", "Close", new CloseCommand());
            Buttons[1] = new Button(new Vector2(Buttons[0].Rec.X + Buttons[0].Rec.Width + 20, 0), "refresh", "Refresh", new RefreshCommand());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatchGraph = new SpriteBatch(GraphicsDevice);
            SpriteBatchUI = new SpriteBatch(GraphicsDevice);
            SCamera = new Camera(GraphicsDevice.Viewport);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SCamera.UpdateCamera(GraphicsDevice.Viewport);
            Filemanager.Update();

            foreach (var btn in Buttons)
                btn.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatchGraph.Begin(
                    SpriteSortMode.Deferred, 
                    BlendState.AlphaBlend,
                    null, null, null, null,
                    SCamera.Transform);

            Filemanager.Draw();

            SpriteBatchGraph.End();

            SpriteBatchUI.Begin();
            foreach (var btn in Buttons)
                btn.Draw();
            SpriteBatchUI.End();

            base.Draw(gameTime);
        }
    }
}
