using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace xnaplatformer
{
    public class Player : Entity
    {
        float jumpSpeed, timer; //bulSpeed
        FileManager fileManager;
        List<Vector2> bul;
        Vector2 temp;
        Texture2D bulImage;
        Game1 g;
        Boolean isRight, full, next; //tt
        public static Boolean first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth;
        SoundEffect back, death, down, jump, select, up;
        
        //float x, y, tempR, tempL;
        //int last;

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

        public Boolean Third
        {
            get { return third; }
            set { third = value; }
        }

        public Boolean Fourth
        {
            get { return fourth; }
            set { fourth = value; }
        }

        public Boolean Fifth
        {
            get { return fifth; }
            set { fifth = value; }
        }

        public Boolean Sixth
        {
            get { return sixth; }
            set { sixth = value; }
        }

        public Boolean Seventh
        {
            get { return seventh; }
            set { seventh = value; }
        }

        public Boolean Eighth
        {
            get { return eighth; }
            set { eighth = value; }
        }

        public Boolean Ninth
        {
            get { return ninth; }
            set { ninth = value; }
        }

        public Boolean Tenth
        {
            get { return tenth; }
            set { tenth = value; }
        }

        public override void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
 	        base.LoadContent(content, attributes, contents, input);
            //string[] attribute = { "PlayerPosition" };
            //string[] ccontent = { position.X.ToString() + "," + position.Y.ToString() };
            fileManager = new FileManager();
            //fileManager.SaveContent("Load/Maps/Map1.gth", attribute, ccontent, "");
            bul = new List<Vector2>();
            temp = new Vector2();
            bulImage = content.Load<Texture2D>("Bullets");
            //tt = true;
            //tempL = 0;
            //tempR = 1600;
            jumpSpeed = 1000f;
            //bulSpeed = 400f;
            timer = 1000000f;
            full = true;
            next = false;
            g = new Game1();
            //t = new Tile();
            select = content.Load<SoundEffect>("Sound/select");
            back = content.Load<SoundEffect>("Sound/back");
            up = content.Load<SoundEffect>("Sound/up");
            down = content.Load<SoundEffect>("Sound/down");
            death = content.Load<SoundEffect>("Sound/death");
            jump = content.Load<SoundEffect>("Sound/jump");
            g.controlSet = System.IO.File.ReadAllText(@"Load\Controls.gth");
            g.difficulty = System.IO.File.ReadAllText(@"Load\Difficulty.gth");
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
            if (g.controlSet == "1")
            {
                if (input.KeyPressed(Keys.Escape))
                {
                    Thread SDown = new Thread(g.SoundFade);
                    SDown.Start();
                    back.Play();
                    Type newClass = Type.GetType("xnaplatformer.TitleScreen");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                    position.X = -100;
                    position.Y = -900;
                    Camera.Instance.SetLocalPosition(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
                }
                if (input.KeyDown(Keys.D))
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
                else if (input.KeyDown(Keys.A))
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
                if (input.KeyDown(Keys.J) && !activateGravity)
                {
                    jump.Play();
                    velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    activateGravity = true;
                }
                if (g.difficulty == "true")
                {
                    if (input.KeyDown(Keys.K) && full)
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
                }
                else if (input.KeyDown(Keys.K))
                //bul.Add(position);
                {
                    moveSpeed = 500f;
                    jumpSpeed = 1200f;
                }
                else
                {
                    moveSpeed = 300f;
                    jumpSpeed = 1000f;
                }
            }
            else if (g.controlSet == "2")
            {
                if (input.KeyPressed(Keys.Escape))
                {
                    Thread SDown = new Thread(g.SoundFade);
                    SDown.Start();
                    back.Play();
                    Type newClass = Type.GetType("xnaplatformer.TitleScreen");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                    position.X = -100;
                    position.Y = -900;
                    Camera.Instance.SetLocalPosition(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
                }
                if (input.KeyDown(Keys.Right))
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
                else if (input.KeyDown(Keys.Left))
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
                if (input.KeyDown(Keys.Z) && !activateGravity)
                {
                    jump.Play();
                    velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    activateGravity = true;
                }
                if (g.difficulty == "true")
                {
                    if (input.KeyDown(Keys.X) && full)
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
                }
                else if (input.KeyDown(Keys.X))
                //bul.Add(position);
                {
                    moveSpeed = 500f;
                    jumpSpeed = 1200f;
                }
                else
                {
                    moveSpeed = 300f;
                    jumpSpeed = 1000f;
                }
            }
            else if (g.controlSet == "3")
            {
                if (input.ButPressed(Buttons.Back))
                {
                    Thread SDown = new Thread(g.SoundFade);
                    SDown.Start();
                    back.Play();
                    Type newClass = Type.GetType("xnaplatformer.TitleScreen");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                    position.X = -100;
                    position.Y = -900;
                    Camera.Instance.SetLocalPosition(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
                }
                if (input.ButDown(Buttons.DPadRight, Buttons.LeftThumbstickRight))
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
                else if (input.ButDown(Buttons.DPadLeft, Buttons.LeftThumbstickLeft))
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
                if (input.ButDown(Buttons.X, Buttons.B) && !activateGravity)
                {
                    jump.Play();
                    velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    activateGravity = true;
                }
                if (g.difficulty == "true")
                {
                    if (input.ButDown(Buttons.A) && full)
                    //bul.Add(position);
                    {
                        timer--;
                        moveSpeed = 500f;
                        jumpSpeed = 1200f;
                    }
                    else
                    {
                        timer++;
                        moveSpeed = 300f;
                        jumpSpeed = 1000f;
                    }
                }
                else if (input.ButDown(Buttons.A))
                    //bul.Add(position);
                    {
                        moveSpeed = 500f;
                        jumpSpeed = 1200f;
                    }
                    else
                    {
                        moveSpeed = 300f;
                        jumpSpeed = 1000f;
                    }
            }
            if (timer < 1000000f)
                full = false;
            else if (timer >= 1000000f)
            {
                timer = 1000000f;
                full = true;
                }
            if (timer <= 0)
                timer = 0f;
            if (first && !second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                if (position.X >= 4000f)
                    next = true;
            if (!first && second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                if (position.X >= 1500f)
                    next = true;
            if (!first && !second && third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                if (position.X >= 1700f)
                    next = true;
            if (!first && !second && !third && fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                if (position.X >= 1700f)
                    next = true;
            if (!first && !second && !third && !fourth && fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                if (position.X <= 0f && position.X > -10f)
                    next = true;
            if (!first && !second && !third && !fourth && !fifth && sixth && !seventh && !eighth && !ninth && !tenth)
                if (position.X >= 4000f)
                    next = true;
            if (!first && !second && !third && !fourth && !fifth && !sixth && seventh && !eighth && !ninth && !tenth)
                if (position.X >= 4000f)
                    next = true;
            if (!first && !second && !third && !fourth && !fifth && !sixth && !seventh && eighth && !ninth && !tenth)
                if (position.X <= 0f && position.X > -10f)
                    next = true;
            if (!first && !second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && ninth && !tenth)
                if (position.X >= 6500f)
                    next = true;
            if (!first && !second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && tenth)
                if (position.X >= 8000f)
                    next = true;
            if (next)
            {
                select.Play();
                Thread SDown = new Thread(g.SoundFade);
                SDown.Start();
                position.X = -100;
                position.Y = -900;
                Camera.Instance.SetLocalPosition(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
                next = false;
                timer = 1000000f;
                if (first && !second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story20");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story30");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story40");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && !third && fourth && !fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story50");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && !third && !fourth && fifth && !sixth && !seventh && !eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story60");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && !third && !fourth && !fifth && sixth && !seventh && !eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story70");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && !third && !fourth && !fifth && !sixth && seventh && !eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story80");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && !third && !fourth && !fifth && !sixth && !seventh && eighth && !ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story90");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && ninth && !tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.Story100");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
                else if (!second && !third && !fourth && !fifth && !sixth && !seventh && !eighth && !ninth && tenth)
                {
                    Type newClass = Type.GetType("xnaplatformer.StoryE0");
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), input);
                }
            }
            #region coment
            /*  if (isRight)
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
            } */
            #endregion
            if (activateGravity)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;
            position += velocity;
            if (health <= 0)
            {
                death.Play();
                position.X = startPosX;
                position.Y = startPosY;
                health = 100;
                timer = 1000000f;
            }
            if (position.Y > 900)
            {
                death.Play();
                health -= 100;
            }
            moveAnimation.Position = position;
            moveAnimation.Update(gameTime); 
            Camera.Instance.SetLocalPosition(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
        }

        public override void OnCollision(Entity e)
        {
            Type type = e.GetType();
            if (type == typeof(Enemy))
            {
                //health -= 30;
                moveAnimation.DrawColor = Color.SlateGray;
                if (health <=0)
                    timer = 1000000f;
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