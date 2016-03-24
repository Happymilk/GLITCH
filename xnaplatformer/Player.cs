using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace xnaplatformer
{
    public class Player : Entity
    {
        float jumpSpeed, timer; //bulSpeed
        FileManager fileManager;
        List<Vector2> bul;
        Vector2 temp;
        Texture2D bulImage;
        Boolean isRight, full, next; //tt
        static Boolean first, second;
        //float x, y, tempR, tempL;
        //int last;
        GameScreen gs;

        public Boolean First
        {
            get { return first; }
            set { first = value; }
        }

        public Boolean Second
        {
            get { return second; }
            set { second = value; }
        }

        public override void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
 	        base.LoadContent(content, attributes, contents, input);
            string[] attribute = { "PlayerPosition" };
            string[] ccontent = { position.X.ToString() + "," + position.Y.ToString() };
            fileManager = new FileManager();
            fileManager.SaveContent("Load/Maps/Map1.gth", attribute, ccontent, "");
            bul = new List<Vector2>();
            temp = new Vector2();
            bulImage = content.Load<Texture2D>("Bullets");
            //tt = true;
            //tempL = 0;
            //tempR = 1600;
            jumpSpeed = 1000f;
            //bulSpeed = 400f;
            timer = 1000f;
            full = true;
            next = false;
            gs = new GameScreen();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            moveAnimation.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager input, Collision col, Layer layer)
        {
            base.Update(gameTime, input, col, layer);
            moveAnimation.DrawColor = Color.White;
            moveAnimation.IsActive = true;
            if (input.KeyPressed(Keys.Escape))
            {
                Type newClass = Type.GetType("xnaplatformer.TitleScreen");
                ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
            }
            if (input.KeyDown(Keys.Right, Keys.D))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 2);
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!isRight || temp.X != position.X)
                {
                    //tt = true;
                    temp.X = position.X;
                }
                isRight = true;
            }
            else if (input.KeyDown(Keys.Left, Keys.A))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (isRight || temp.X != position.X)
                {
                    //tt = true;
                    temp.X = position.X;
                }
                isRight = false;
            }
            else
            {
                moveAnimation.IsActive = false;
                velocity.X = 0;
            }
            if (input.KeyDown(Keys.Z, Keys.J) && !activateGravity)
            {
                velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                activateGravity = true;
            }
            if (input.KeyDown(Keys.X, Keys.K) && full)
                //bul.Add(position);
            {
                timer--;
                moveSpeed = 500f;
                jumpSpeed = 1200f;
            }
            else
            {
                moveSpeed = 300f;
                jumpSpeed = 1000f;
            }
            if (timer < 1000f)
            {
                full = false;
                timer++;
            }
            else if (timer == 1000f)
                full = true;
            if (timer <= 0)
                timer = 0f;
            first = true;
            if (first && !second)
                if (position.X >= 1000f)
                    next = true;
            if (second)
                if (position.X >= 100f)
                    next = true;
            if (next)
            {
                if (first && !second)
                {
                    second = true;
                    next = false;
                    Type newClass = Type.GetType("xnaplatformer.Lvl2");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (second)
                {
                    next = false;
                    Type newClass = Type.GetType("xnaplatformer.TitleScreen");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
            }
            /*if (isRight)
            {
                for (int i = 0; i < bul.Count; i++)
                {
                    last = i;
                    x = bul[i].X;
                    y = bul[i].Y;
                    if (tt)
                    {
                        tempR = x + 100;
                        tt = false;
                    }
                    if (bul[last].X < tempR)
                    {
                        x += bulSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        bul[i] = new Vector2(x, y);
                    }
                    else if (bul[last].X >= tempR)
                        bul.Remove(bul[last]);
                }
            }
            else
            {
                for (int i = 0; i < bul.Count; i++)
                {
                    last = i;
                    x = bul[i].X;
                    y = bul[i].Y;
                    if (tt)
                    {
                        tempL = x - 100;
                        tt = false;
                    }
                    if (bul[last].X > tempL)
                    {
                        x -= bulSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        bul[i] = new Vector2(x, y);
                    }
                    else if (bul[last].X <= tempL)
                        bul.Remove(bul[last]);
                }
            }*/
            if (activateGravity)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;
            position += velocity;
            if (health <= 0)
            {
                position.X = startPosX;
                position.Y = startPosY;
                health = 100;
                timer = 1000f;
            }
            if (position.Y > 900)
                health -= 100;
            moveAnimation.Position = position;
            moveAnimation.Update(gameTime); 
            Camera.Instance.SetLocalPosition(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
        }

        public override void OnCollision(Entity e)
        {
            Type type = e.GetType();
            if (type == typeof(Enemy))
            {
                health -= 30;
                moveAnimation.DrawColor = Color.SlateGray;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
            for (int i = 0; i < bul.Count; i++)
                spriteBatch.Draw(bulImage, bul[i], Color.White);
        }
    }
}