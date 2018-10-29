using System;
using System.Collections.Generic;
using Arkanoid.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.States
{
    public class GameState : State
    {
        #region Fields
        private List<Component> _components;
        private ScoreBox _scoreBox;
        private Ball _ball;
        private Paddle _paddle;
        private List<Block> _listOfBlocks = new List<Block> { };
        #endregion

        #region Methods
        private void CheckIfBallCollideWithAnyBlock()
        {
            foreach (Block block in _listOfBlocks)
            {
                if (_ball.Rectangle.Intersects(block.Rectangle))
                {
                    
                    if (block.State == 3)
                        block.State = 2;
                    else if (block.State == 2)
                        block.State = 1;
                    else if (block.State == 1)
                        block.State = 1;
                }
            }
        }

        private void CheckIfBallCollideWithPaddle()
        {
            if (_ball.Rectangle.Intersects(_paddle.Rectangle))
            {
                _ball.DirectionY = Math.Abs(_ball.DirectionY) * -1;
            }
        }

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var paddleTextureStandard = _content.Load<Texture2D>("Objects/PaddleStandard");
            _paddle = new Paddle(paddleTextureStandard, new Vector2(320, 450));

            var ballTexture = _content.Load<Texture2D>("Objects/Ball");
            _ball = new Ball(ballTexture)
            { Position = new Vector2(392, 434) };

            //Need new texture
            var scoreBoxTexture = _content.Load<Texture2D>("Controls/Button");
            var scoreBoxFont = _content.Load<SpriteFont>("Fonts/Font");
            _scoreBox = new ScoreBox(scoreBoxTexture, scoreBoxFont)
            { Position = new Vector2(795 - scoreBoxTexture.Width, 5) };
            

            var blockTexture = _content.Load<Texture2D>("Objects/Block");
            #region ToDestroy
            var block1 = new Block(blockTexture, 1) { Position = new Vector2(70, 70) }; _listOfBlocks.Add(block1);
            var block2 = new Block(blockTexture, 1) { Position = new Vector2(115, 70) }; _listOfBlocks.Add(block2);
            var block3 = new Block(blockTexture, 2) { Position = new Vector2(160, 70) }; _listOfBlocks.Add(block3);
            var block4 = new Block(blockTexture, 3) { Position = new Vector2(205, 70) }; _listOfBlocks.Add(block4);
            var block5 = new Block(blockTexture, 2) { Position = new Vector2(250, 70) }; _listOfBlocks.Add(block5);
            var block6 = new Block(blockTexture, 3) { Position = new Vector2(295, 70) }; _listOfBlocks.Add(block6);
            var block7 = new Block(blockTexture, 1) { Position = new Vector2(340, 70) }; _listOfBlocks.Add(block7);
            var block8 = new Block(blockTexture, 3) { Position = new Vector2(385, 70) }; _listOfBlocks.Add(block8);
            var block9 = new Block(blockTexture, 3) { Position = new Vector2(430, 70) }; _listOfBlocks.Add(block9);
            var block10 = new Block(blockTexture, 2) { Position = new Vector2(475, 70) }; _listOfBlocks.Add(block10);
            var block11 = new Block(blockTexture, 1) { Position = new Vector2(520, 70) }; _listOfBlocks.Add(block11);
            var block12 = new Block(blockTexture, 3) { Position = new Vector2(565, 70) }; _listOfBlocks.Add(block12);
            var block13 = new Block(blockTexture, 2) { Position = new Vector2(610, 70) }; _listOfBlocks.Add(block13);
            var block14 = new Block(blockTexture, 1) { Position = new Vector2(655, 70) }; _listOfBlocks.Add(block14);
            var block15 = new Block(blockTexture, 3) { Position = new Vector2(700, 70) }; _listOfBlocks.Add(block15);
            #endregion
            #region Can'tBeDestroyed
            var block16 = new Block(blockTexture, 4) { Position = new Vector2(160, 100) }; _listOfBlocks.Add(block16);
            var block17 = new Block(blockTexture, 4) { Position = new Vector2(205, 100) }; _listOfBlocks.Add(block17);
            var block18 = new Block(blockTexture, 4) { Position = new Vector2(250, 100) }; _listOfBlocks.Add(block18);
            var block19 = new Block(blockTexture, 4) { Position = new Vector2(295, 100) }; _listOfBlocks.Add(block19);
            var block20 = new Block(blockTexture, 4) { Position = new Vector2(340, 100) }; _listOfBlocks.Add(block20);
            var block21 = new Block(blockTexture, 4) { Position = new Vector2(385, 100) }; _listOfBlocks.Add(block21);
            var block22 = new Block(blockTexture, 4) { Position = new Vector2(430, 100) }; _listOfBlocks.Add(block22);
            var block23 = new Block(blockTexture, 4) { Position = new Vector2(475, 100) }; _listOfBlocks.Add(block23);
            var block24 = new Block(blockTexture, 4) { Position = new Vector2(520, 100) }; _listOfBlocks.Add(block24);
            var block25 = new Block(blockTexture, 4) { Position = new Vector2(565, 100) }; _listOfBlocks.Add(block25);
            #endregion

            _components = new List<Component>()
            {
                _paddle,
                _ball,
                block1,block6,block11,block16,block21,
                block2,block7,block12,block17,block22,
                block3,block8,block13,block18,block23,
                block4,block9,block14,block19,block24,
                block5,block10,block15,block20,block25,
                _scoreBox,
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

            CheckIfBallCollideWithPaddle();
            CheckIfBallCollideWithAnyBlock();
        }
        #endregion
    }
}
