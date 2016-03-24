using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace xnaplatformer
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public string controlSet, difficulty;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Data";
        }

        protected override void Initialize()
        {
            ScreenManager.Instance.Initialize();
            ScreenManager.Instance.Dimensions = new Vector2(1600, 896);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.LoadContent(Content);
            controlSet = System.IO.File.ReadAllText(@"Load\Controls.gth");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.LeftStick == ButtonState.Pressed &&
                GamePad.GetState(PlayerIndex.One).Buttons.RightStick == ButtonState.Pressed)
            {
                for (int i = 0; i < 50000; i++)
                {
                    MediaPlayer.Volume -= 0.00002f;
                    if (MediaPlayer.Volume == 0f)
                        this.Exit();
                }
            }
            ScreenManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Camera.Instance.ViewMatrix);
            ScreenManager.Instance.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SoundFade()
        {
            for (int i = 0; i < 10000; i++)
            {
                MediaPlayer.Volume -= 0.0001f;
                if (MediaPlayer.Volume == 0f)
                    MediaPlayer.Pause();
            }
        }
    }
}