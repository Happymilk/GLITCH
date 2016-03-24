using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace xnaplatformer
{
    public class InputManager
    {
        KeyboardState prevKeyState, keyState;
        GamePadState prevButState, butState;

        public KeyboardState PrevKeyState
        {
            get { return prevKeyState; }
            set { prevKeyState = value; }
        }

        public GamePadState PrevButState
        {
            get { return prevButState; }
            set { prevButState = value; }
        }

        public KeyboardState KeyState
        {
            get { return keyState; }
            set { keyState = value; }
        }

        public GamePadState ButState
        {
            get { return butState; }
            set { butState = value; }
        }

        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            prevButState = butState;
            butState = GamePad.GetState(PlayerIndex.One);
        }

        public bool KeyPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyUp(Keys key)
        {
            if (keyState.IsKeyUp(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyUp(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyUp(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(Keys key)
        {
            if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
                return true;
            else
                return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool ButPressed(Buttons but)
        {
            if (butState.IsButtonDown(but) && prevButState.IsButtonUp(but)) 
                return true;
            return false;
        }

        public bool ButPressed(params Buttons[] buts)
        {
            foreach (Buttons but in buts)
            {
                if (butState.IsButtonDown(but) && prevButState.IsButtonUp(but))
                    return true;
            }
            return false;
        }

        public bool ButReleased(Buttons but)
        {
            if (butState.IsButtonUp(but) && prevButState.IsButtonDown(but))
                return true;
            return false;
        }

        public bool ButReleased(params Buttons[] buts)
        {
            foreach (Buttons but in buts)
            {
                if (butState.IsButtonUp(but) && prevButState.IsButtonDown(but))
                    return true;
            }
            return false;
        }

        public bool ButDown(Buttons but)
        {
            if (butState.IsButtonDown(but))
                return true;
            else
                return false;
        }

        public bool ButDown(params Buttons[] buts)
        {
            foreach (Buttons but in buts)
            {
                if (butState.IsButtonDown(but))
                    return true;
            }
            return false;
        }
    }
}