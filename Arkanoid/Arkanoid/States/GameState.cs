using System;
using System.Collections.Generic;
using Arkanoid.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid.States
{
    public class GameState : State
    {
        private List<Component> _components;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
            :  base(game, graphicsDevice, content)
        {
            var paddleTexture = _content.Load<Texture2D>("Objects/Paddle");
            var blockTexture = _content.Load<Texture2D>("Objects/Block");
            var ballTexture = _content.Load<Texture2D>("Objects/Ball");

            var paddle = new Paddle(paddleTexture)
            {
                Position = new Vector2(320, 450),
            };

            var ball = new Ball(ballTexture)
            {
                Position = new Vector2(392, 434),
            };

            //Test Blocks:
            var block4 = new Block(blockTexture)
            {
                Position = new Vector2(20, 20),
                State = 4,
            };
            var block3 = new Block(blockTexture)
            {
                Position = new Vector2(60, 20),
                State = 3,
            };
            var block2 = new Block(blockTexture)
            {
                Position = new Vector2(100, 20),
                State = 2,
            };
            var block1 = new Block(blockTexture)
            {
                Position = new Vector2(140, 20),
            };

            _components = new List<Component>()
            {
                paddle,
                ball,
                block4,
                block3,
                block2,
                block1,
            };
        }  

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //TODO: Delete sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
