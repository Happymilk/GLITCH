﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace xnaplatformer
{
    public class EntityManager
    {
        List<Entity> entities;
        FileManager fileManager;
        InputManager input;
        List<List<string>> attributes, contents;

        public List<Entity> Entities
        {
            get { return entities; }
        }

        public void LoadContent(string entityType, ContentManager Content, string fileName, string identifier, InputManager input)
        {
            this.input = input;
            entities = new List<Entity>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            fileManager = new FileManager();
            if (identifier == String.Empty)
                fileManager.LoadContent(fileName, attributes, contents, "");
            else
                fileManager.LoadContent(fileName, attributes, contents, identifier);
            for (int i = 0; i < attributes.Count; i++)
            {
                Type newClass = Type.GetType("xnaplatformer." + entityType);
                entities.Add((Entity)Activator.CreateInstance(newClass));
                entities[i].LoadContent(Content, attributes[i], contents[i], this.input);
            }
        }

        public void UnloadContent()
        {
            for (int i = 0; i < entities.Count; i++)
                entities[i].UnloadContent();
        }

        public void Update(GameTime gameTime, Map map)
        {
            for (int i = 0; i < entities.Count; i++)
                entities[i].Update(gameTime, input, map.collision, map.layer);
        }

        public void EntityCollision(EntityManager E2)
        {
            foreach (Entity e in entities)
            {
                foreach (Entity e2 in E2.Entities)
                {
                    if (e.Rect.Intersects(e2.Rect))
                        e.OnCollision(e2);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < entities.Count; i++)
                entities[i].Draw(spriteBatch);
        }
    }
}