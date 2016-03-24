using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace xnaplatformer
{
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<FadeAnimation> fade;
        List<Texture2D> images;
        FileManager fileManager;
        int imageNumber;
        
        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("Font");
            imageNumber = 0;
            fileManager = new FileManager();
            fade = new List<FadeAnimation>();
            images = new List<Texture2D>();
            fileManager.LoadContent("Load/Splash.gth", attributes, contents, "");
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents[i][j]));
                            fade.Add(new FadeAnimation());
                            break;
                    }
                }
            }
            for (int i = 0; i < attributes.Count; i++)
            {
                fade[i].LoadContent(content, images[i], "", new Vector2(ScreenManager.Instance.Dimensions.X / 2 - 
                    images[i].Width / 2, ScreenManager.Instance.Dimensions.Y / 2 - images[i].Height / 2));
                fade[i].Scale = 1.0f;
                fade[i].IsActive = false;
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            fade[imageNumber].Update(gameTime);
            if (fade[imageNumber].Alpha == 0.0f)
                imageNumber++;
            if (imageNumber >= fade.Count - 1 || 
                inputManager.KeyPressed(Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.A,         
                Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3,
                Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9, Keys.Up, Keys.Down, Keys.S,
                Keys.Left, Keys.Right, Keys.Escape, Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.G,
                Keys.F9, Keys.F10, Keys.F11, Keys.F12, Keys.PrintScreen, Keys.Pause, Keys.Insert, Keys.Delete, Keys.Home, Keys.D,
                Keys.PageUp, Keys.PageDown, Keys.End, Keys.OemTilde, Keys.OemSemicolon, Keys.OemQuotes, Keys.OemQuestion, Keys.F,
                Keys.OemPlus, Keys.OemPipe, Keys.OemPeriod, Keys.OemOpenBrackets, Keys.OemMinus, Keys.OemEnlW, Keys.OemCopy, Keys.H,
                Keys.OemComma, Keys.OemCloseBrackets, Keys.OemClear, Keys.OemBackslash, Keys.OemAuto, Keys.Oem8, Keys.Multiply, 
                Keys.Decimal, Keys.Add, Keys.Back, Keys.CapsLock, Keys.Crsel, Keys.Divide,Keys.EraseEof, Keys.Escape, Keys.Execute,
                Keys.Exsel, Keys.Help, Keys.ImeConvert, Keys.ImeNoConvert, Keys.LeftAlt, Keys.LeftControl, Keys.LeftShift, Keys.J,
                Keys.LeftWindows, Keys.Pa1, Keys.Print, Keys.RightAlt, Keys.RightControl, Keys.RightShift, Keys.RightWindows, Keys.K,
                Keys.Scroll, Keys.Select, Keys.Separator, Keys.Sleep, Keys.Tab, Keys.Enter, Keys.L, Keys.Space) || 
                inputManager.ButPressed(Buttons.A, Buttons.B, Buttons.Back, Buttons.DPadDown, Buttons.DPadLeft, Buttons.DPadRight,
                Buttons.DPadUp, Buttons.LeftShoulder, Buttons.LeftStick, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickLeft,
                Buttons.LeftThumbstickRight, Buttons.LeftThumbstickUp, Buttons.LeftTrigger, Buttons.RightShoulder, Buttons.RightStick,
                Buttons.RightThumbstickDown, Buttons.RightThumbstickLeft, Buttons.RightThumbstickRight, Buttons.RightThumbstickUp, 
                Buttons.RightTrigger, Buttons.Start, Buttons.X, Buttons.Y, Buttons.BigButton))
            {
                if (fade[imageNumber].Alpha != 1.0f)
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager, fade[imageNumber].Alpha);
                else
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fade[imageNumber].Draw(spriteBatch);
        }
    }
}