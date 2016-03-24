using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace xnaplatformer
{
    class Enemy : Entity
    {
        public override void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
            base.LoadContent(content, attributes, contents, input);
            direction = 1;
            origPosition = position;
            if (direction == 1)
                destPosition.X = origPosition.X + range;
            else
                destPosition.X = origPosition.X - range;
            moveAnimation.IsActive = true;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager input, Collision col, Layer layer)
        {
            base.Update(gameTime, input, col, layer);
            if (direction == 1)
            {
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 2);
            }
            else if (direction == 2)
            {
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
            }
            if (activateGravity)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;
            position += velocity;
            if (direction == 1 && position.X >= destPosition.X)
            {
                direction = 2;
                destPosition.X = origPosition.X - range;
            }
            else if (direction == 2 && position.X <= destPosition.X)
            {
                direction = 1;
                destPosition.X = origPosition.X + range;
            }
            if (position.Y > 900)
                position.Y = 900;
            moveAnimation.Update(gameTime);
            moveAnimation.Position = position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
        }
    }
}