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
            : base(game, graphicsDevice, content)
        {
            var paddleTextureStandard = _content.Load<Texture2D>("Objects/PaddleStandard");

            var blockTexture = _content.Load<Texture2D>("Objects/Block");
            var ballTexture = _content.Load<Texture2D>("Objects/Ball");

            var paddle = new Paddle(paddleTextureStandard, new Vector2(320, 450));
            var ball = new Ball(ballTexture) { Position = new Vector2(392, 434) };

            #region ToDestroy
            var block1 = new Block(blockTexture, 1) { Position = new Vector2(70, 70) };
            var block2 = new Block(blockTexture, 1) { Position = new Vector2(115, 70) };
            var block3 = new Block(blockTexture, 2) { Position = new Vector2(160, 70) };
            var block4 = new Block(blockTexture, 3) { Position = new Vector2(205, 70) };
            var block5 = new Block(blockTexture, 2) { Position = new Vector2(250, 70) };
            var block6 = new Block(blockTexture, 3) { Position = new Vector2(295, 70) };
            var block7 = new Block(blockTexture, 1) { Position = new Vector2(340, 70) };
            var block8 = new Block(blockTexture, 3) { Position = new Vector2(385, 70) };
            var block9 = new Block(blockTexture, 3) { Position = new Vector2(430, 70) };
            var block10 = new Block(blockTexture, 2) { Position = new Vector2(475, 70) };
            var block11 = new Block(blockTexture, 1) { Position = new Vector2(520, 70) };
            var block12 = new Block(blockTexture, 3) { Position = new Vector2(565, 70) };
            var block13 = new Block(blockTexture, 2) { Position = new Vector2(610, 70) };
            var block14 = new Block(blockTexture, 1) { Position = new Vector2(655, 70) };
            var block15 = new Block(blockTexture, 3) { Position = new Vector2(700, 70) };
            #endregion

            #region Can'tBeDestroyed
            var block16 = new Block(blockTexture, 4) { Position = new Vector2(160, 100) };
            var block17 = new Block(blockTexture, 4) { Position = new Vector2(205, 100) };
            var block18 = new Block(blockTexture, 4) { Position = new Vector2(250, 100) };
            var block19 = new Block(blockTexture, 4) { Position = new Vector2(295, 100) };
            var block20 = new Block(blockTexture, 4) { Position = new Vector2(340, 100) };
            var block21 = new Block(blockTexture, 4) { Position = new Vector2(385, 100) };
            var block22 = new Block(blockTexture, 4) { Position = new Vector2(430, 100) };
            var block23 = new Block(blockTexture, 4) { Position = new Vector2(475, 100) };
            var block24 = new Block(blockTexture, 4) { Position = new Vector2(520, 100) };
            var block25 = new Block(blockTexture, 4) { Position = new Vector2(565, 100) };
            #endregion

            _components = new List<Component>()
            {
                paddle,
                ball,
                block1,block6,block11,block16,block21,
                block2,block7,block12,block17,block22,
                block3,block8,block13,block18,block23,
                block4,block9,block14,block19,block24,
                block5,block10,block15,block20,block25,
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
