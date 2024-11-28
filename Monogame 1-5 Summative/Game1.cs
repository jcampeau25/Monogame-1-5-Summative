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

        Rectangle window, kanyeRect1, kanyeRect2, swiftRect, cityRect1, cityRect2;

        Texture2D introTexture, kanyeTexture, stageTexture, swiftTexture, interuptionTexture, cityTexture;

        Vector2 kanyeSpeed, kanyeRunSpeed, backgroundSpeed;

        Screen screen;

        SpriteFont instructionsFont;

        MouseState mouseState;

        float seconds = 0f;

        SoundEffect interupt, mob;
        SoundEffectInstance interuptInstance, mobInstance;
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
            kanyeRect1 = new Rectangle(100, 400, 100, 100);
            kanyeRect2 = new Rectangle(610, 365, 100, 100);
            kanyeSpeed = new Vector2(2, 2);
            kanyeRunSpeed = new Vector2(0, 3);
            swiftRect = new Rectangle(340, 295, 75, 100);
            cityRect1 = new Rectangle(0, 0, 800, 500);
            cityRect2 = new Rectangle(800, 0, 800, 500);

            backgroundSpeed = new Vector2(5, 0);

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
            cityTexture = Content.Load<Texture2D>("citystreet");
            instructionsFont = Content.Load<SpriteFont>("InstructionFont");
            interupt = Content.Load<SoundEffect>("interuptionsound");
            interuptInstance = interupt.CreateInstance();
            mob = Content.Load<SoundEffect>("angryMobSound");
            mobInstance = mob.CreateInstance();
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
                if (kanyeRect1.X < 255)
                {
                    kanyeRect1.X += (int)kanyeSpeed.X;
                }
                else if (kanyeRect1.X >= 255 && kanyeRect1.Y > 305)
                    kanyeRect1.Y -= (int)kanyeSpeed.Y;
                if (seconds >= 2.5 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    interuptInstance.Play();
                    screen = Screen.Interuption;
                }
            }

            else if (screen == Screen.Interuption)
            {
                if (interuptInstance.State == SoundState.Stopped)
                {
                    mobInstance.Play();
                    mobInstance.IsLooped = true;
                    screen = Screen.Chase;
                }
            }
            
            else if (screen == Screen.Chase)
            {
                cityRect1.X -= (int)backgroundSpeed.X;
                cityRect2.X -= (int)backgroundSpeed.X;
                kanyeRect2.Y += (int)kanyeSpeed.Y;

                if (cityRect1.Right < 0)
                {
                    cityRect1.X = 800;
                }
                if (cityRect2.Right < 0)
                {
                    cityRect2.X = 800;
                }

                if (kanyeRect2.Bottom >= 470 || kanyeRect2.Top <= 400)
                    kanyeRunSpeed.Y *= -1;
            }

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
                _spriteBatch.DrawString(instructionsFont, ("WELCOME TO THE 2009 VMAS"), new Vector2(60, 150), Color.YellowGreen);
                _spriteBatch.DrawString(instructionsFont, ("Click to Start"), new Vector2(250, 250), Color.White);

            }

            else if (screen == Screen.Stage)
            {
                _spriteBatch.Draw(stageTexture, window, Color.White);
                _spriteBatch.Draw(kanyeTexture, kanyeRect1, Color.White);
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

            else if (screen == Screen.Chase)
            {
                _spriteBatch.Draw(cityTexture, cityRect1, Color.White);
                _spriteBatch.Draw(cityTexture, cityRect2, Color.White);
                _spriteBatch.Draw(kanyeTexture, kanyeRect2, Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
