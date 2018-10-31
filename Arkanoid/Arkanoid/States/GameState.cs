using System;
using System.Collections.Generic;
using Arkanoid.Controls;
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
        private Background _gameStateBackground;
        private Ball _ball;
        private Paddle _paddle;
        private List<Block> _listOfBlocks = new List<Block> { };
        private List<Block> _listOfBlocksToDelete = new List<Block> { };
        #endregion

        #region Methods 
        private void CheckIfBallCollideWithAnyBlock()
        {
            foreach(Block block in _listOfBlocks)
            {
                if (_ball.Rectangle.Intersects(block.Rectangle))
                {
                    float distanceVertical;
                    float distanceHorizontal;
               
                    //Ball comes from upper left
                    if (_ball.OldRectangle.Center.X <= block.Rectangle.Center.X &&
                        _ball.OldRectangle.Center.Y <= block.Rectangle.Center.Y)
                    {
                        distanceVertical = Math.Abs((block.Rectangle.Top - _ball.OldRectangle.Bottom) * _ball.DirectionY);
                        distanceHorizontal = Math.Abs((block.Rectangle.Left - _ball.OldRectangle.Right) * _ball.DirectionX);  
                    }
                    //Ball comes from upper right
                    else if (_ball.OldRectangle.Center.X >= block.Rectangle.Center.X &&
                             _ball.OldRectangle.Center.Y <= block.Rectangle.Center.Y)
                    {
                        distanceVertical = Math.Abs((block.Rectangle.Top - _ball.OldRectangle.Bottom) * _ball.DirectionY);
                        distanceHorizontal = Math.Abs((_ball.OldRectangle.Left - block.Rectangle.Right) * _ball.DirectionX);
                    }
                    //Ball comes from bottom left
                    else if (_ball.OldRectangle.Center.X <= block.Rectangle.Center.X &&
                             _ball.OldRectangle.Center.Y >= block.Rectangle.Center.Y)
                    {
                        distanceVertical = Math.Abs((_ball.OldRectangle.Top - block.Rectangle.Bottom) * _ball.DirectionY);
                        distanceHorizontal = Math.Abs((block.Rectangle.Left - _ball.OldRectangle.Right) * _ball.DirectionX);
                    }
                    //Ball comes from bottom right
                    else
                    {
                        distanceVertical = Math.Abs((_ball.OldRectangle.Top - block.Rectangle.Bottom) * _ball.DirectionY);
                        distanceHorizontal = Math.Abs((_ball.OldRectangle.Left - block.Rectangle.Right) * _ball.DirectionX);
                    }

                    if (distanceVertical > distanceHorizontal)
                        _ball.DirectionX = _ball.DirectionX * -1;
                    else
                        _ball.DirectionY = _ball.DirectionY * -1;

                    if (block.State == 3)
                        block.State = 2;
                    else if (block.State == 2)
                        block.State = 1;
                    else if (block.State == 1)
                        _listOfBlocks.Remove(block);
                        
                    break;
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

        private void CheckIfGameIsWon()
        {
            bool gameWonFlag = true;

            foreach(Block block in _listOfBlocks)
            {
                if (block.State != 4)
                {
                    gameWonFlag = false;
                }
            }

            if (gameWonFlag)
                _game.ChangeState(new VictoryState(_game, _graphicsDevice, _content));
        }

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var gameStateBackgroundTexture = _content.Load<Texture2D>("Backgrounds/GameStateBackground");
            _gameStateBackground = new Background(gameStateBackgroundTexture);

            var paddleTextureStandard = _content.Load<Texture2D>("Objects/PaddleStandard");
            _paddle = new Paddle(paddleTextureStandard, new Vector2(200, 660));

            var ballTexture = _content.Load<Texture2D>("Objects/Ball");
            _ball = new Ball(ballTexture)
            { Position = new Vector2(262, 644) };         

            var blockTexture = _content.Load<Texture2D>("Objects/Block");
            #region ToDestroy
            var block1 = new Block(blockTexture, 1) { Position = new Vector2(100, 180) }; _listOfBlocks.Add(block1);
            var block2 = new Block(blockTexture, 1) { Position = new Vector2(140, 180) }; _listOfBlocks.Add(block2);
            //var block3 = new Block(blockTexture, 2) { Position = new Vector2(160, 70) }; _listOfBlocks.Add(block3);
            //var block4 = new Block(blockTexture, 3) { Position = new Vector2(205, 70) }; _listOfBlocks.Add(block4);
            //var block5 = new Block(blockTexture, 2) { Position = new Vector2(250, 70) }; _listOfBlocks.Add(block5);
            //var block6 = new Block(blockTexture, 3) { Position = new Vector2(295, 70) }; _listOfBlocks.Add(block6);
            //var block7 = new Block(blockTexture, 1) { Position = new Vector2(340, 70) }; _listOfBlocks.Add(block7);
            //var block8 = new Block(blockTexture, 3) { Position = new Vector2(385, 70) }; _listOfBlocks.Add(block8);
            //var block9 = new Block(blockTexture, 3) { Position = new Vector2(430, 70) }; _listOfBlocks.Add(block9);
            //var block10 = new Block(blockTexture, 2) { Position = new Vector2(475, 70) }; _listOfBlocks.Add(block10);
            //var block11 = new Block(blockTexture, 1) { Position = new Vector2(520, 70) }; _listOfBlocks.Add(block11);
            //var block12 = new Block(blockTexture, 3) { Position = new Vector2(565, 70) }; _listOfBlocks.Add(block12);
            //var block13 = new Block(blockTexture, 2) { Position = new Vector2(610, 70) }; _listOfBlocks.Add(block13);
            //var block14 = new Block(blockTexture, 1) { Position = new Vector2(655, 70) }; _listOfBlocks.Add(block14);
            //var block15 = new Block(blockTexture, 3) { Position = new Vector2(700, 70) }; _listOfBlocks.Add(block15);
            #endregion
            #region Can'tBeDestroyed
            var block16 = new Block(blockTexture, 4) { Position = new Vector2(100, 200) }; _listOfBlocks.Add(block16);
            var block17 = new Block(blockTexture, 4) { Position = new Vector2(140, 200) }; _listOfBlocks.Add(block17);
            var block18 = new Block(blockTexture, 4) { Position = new Vector2(180, 200) }; _listOfBlocks.Add(block18);
            var block19 = new Block(blockTexture, 4) { Position = new Vector2(220, 200) }; _listOfBlocks.Add(block19);
            var block20 = new Block(blockTexture, 4) { Position = new Vector2(260, 200) }; _listOfBlocks.Add(block20);
            var block21 = new Block(blockTexture, 4) { Position = new Vector2(300, 200) }; _listOfBlocks.Add(block21);
            var block22 = new Block(blockTexture, 4) { Position = new Vector2(340, 200) }; _listOfBlocks.Add(block22);
            var block23 = new Block(blockTexture, 4) { Position = new Vector2(380, 200) }; _listOfBlocks.Add(block23);
            var block24 = new Block(blockTexture, 4) { Position = new Vector2(420, 200) }; _listOfBlocks.Add(block24);
            var block25 = new Block(blockTexture, 4) { Position = new Vector2(460, 200) }; _listOfBlocks.Add(block25);
            var block26 = new Block(blockTexture, 4) { Position = new Vector2(500, 200) }; _listOfBlocks.Add(block26);
            #endregion
            _listOfBlocksToDelete = new List<Block> { };

            _components = new List<Component>()
            {
                _gameStateBackground,
                _paddle,
                _ball,
            };

        } 
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            foreach (Block block in _listOfBlocks)
                block.Draw(gameTime, spriteBatch);

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

            foreach (Block block in _listOfBlocks)
                block.Update(gameTime);

            CheckIfBallCollideWithPaddle();
            CheckIfBallCollideWithAnyBlock();
            CheckIfGameIsWon();
        }

        
        #endregion
    }
}
