using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace xnaplatformer
{
    class Lvl3 : GameScreen
    {
        EntityManager player, enemies;
        Map map;
        Player p;
        Song music;

        public override void LoadContent(ContentManager content, InputManager input)
        {
            base.LoadContent(content, input);
            player = new EntityManager();
            enemies = new EntityManager();
            map = new Map();
            map.LoadContent(content, map, "Map3");
            player.LoadContent("Player", content, "Load/Player/Level3.gth", "", input);
            enemies.LoadContent("Enemy", content, "Load/Enemies/Level3.gth", "Level3", input);
            p = new Player();
            p.First = false;
            p.Second = false;
            p.Third = true;
            p.Fourth = false;
            p.Fifth = false;
            p.Sixth = false;
            p.Seventh = false;
            p.Eighth = false;
            p.Ninth = false;
            p.Tenth = false;
            Thread SDown = new Thread(goMusic);
            SDown.Start();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime, map);
            enemies.Update(gameTime, map);
            map.Update(gameTime);
            Entity e;
            for (int i = 0; i < player.Entities.Count; i++)
            {
                e = player.Entities[i];
                map.UpdateCollision(ref e);
                player.Entities[i] = e;
            }
            for (int i = 0; i < enemies.Entities.Count; i++)
            {
                e = enemies.Entities[i];
                map.UpdateCollision(ref e);
                enemies.Entities[i] = e;
            }
            player.EntityCollision(enemies);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemies.Draw(spriteBatch);
        }

        private void goMusic()
        {
            music = content.Load<Song>("Music/06 - Morte");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1f;
        }
    }
}