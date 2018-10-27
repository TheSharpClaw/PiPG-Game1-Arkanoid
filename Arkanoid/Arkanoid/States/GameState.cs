using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid.States
{
    public class GameState : State
    {
        private List<Component> _components;
        private SpriteFont _font;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
            :  base(game, graphicsDevice, content)
        {
            
        }  

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();



            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //Delete sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
