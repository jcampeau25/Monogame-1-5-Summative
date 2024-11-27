using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_1_5_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window, kanyeRect, swiftRect;

        Texture2D introTexture, kanyeTexture, stageTexture, swiftTexture, interuptionTexture;

        Vector2 kanyeSpeed;

        Screen screen;

        SpriteFont instructionsFont;

        MouseState mouseState;

        float seconds = 0f;

        SoundEffect interupt;
        SoundEffectInstance interuptInstance;
        enum Screen
        {
            Intro,
            Interuption,
            Stage,
            Chase
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            window = new Rectangle(0, 0, 800, 500);
            kanyeRect = new Rectangle(100, 400, 100, 100);
            kanyeSpeed = new Vector2(2, 2);
            swiftRect = new Rectangle(340, 295, 75, 100);



            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight= 500;
            screen = Screen.Intro;


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            introTexture = Content.Load<Texture2D>("vmaIntro");
            stageTexture = Content.Load<Texture2D>("vmaStage");
            kanyeTexture = Content.Load<Texture2D>("vmaKanye");
            swiftTexture = Content.Load<Texture2D>("vmaTaylor");
            interuptionTexture = Content.Load<Texture2D>("interuption");
            instructionsFont = Content.Load<SpriteFont>("InstructionFont");
            //interupt = Content.Load<SoundEffect>("interuptionsound");
            //interuptInstance = interupt.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}" + "    " + seconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Stage;
                }
            }

            else if (screen == Screen.Stage)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (kanyeRect.X < 255)
                {
                    kanyeRect.X += (int)kanyeSpeed.X;
                }
                else if (kanyeRect.X >= 255 && kanyeRect.Y > 305)
                    kanyeRect.Y -= (int)kanyeSpeed.Y;
                if (seconds >= 2.5 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    
                    screen = Screen.Interuption;
                }
            }
            //else if (screen == Screen.Interuption)



                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introTexture, window, Color.White);
            }

            else if (screen == Screen.Stage)
            {
                _spriteBatch.Draw(stageTexture, window, Color.White);
                _spriteBatch.Draw(kanyeTexture, kanyeRect, Color.White);
                _spriteBatch.Draw(swiftTexture, swiftRect, Color.White);

                if (seconds > 2.5)
                {
                    _spriteBatch.DrawString(instructionsFont, ("Click To Interupt Taylor"), new Vector2(10, 10), Color.White);
                }
            }

            else if (screen == Screen.Interuption)
            {
                _spriteBatch.Draw(interuptionTexture, window, Color.White);
            }
         
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
