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
        private int _lifeCounter;
        private int _score;
        private SpriteFont _font;
        private List<Component> _components;
        private Background _gameStateBackground;
        private Paddle _paddle;
        private Texture2D _ballTexture;
        private List<Ball> _listOfBalls = new List<Ball> { };
        private List<Block> _listOfBlocks = new List<Block> { };
        #endregion

        #region Methods 
        private void CheckBalls()
        {
            if (_listOfBalls.Count > 0)
            {
                foreach (Ball ball in _listOfBalls)
                {
                    if (ball.Position.Y > 680)
                    {
                        _listOfBalls.Remove(ball);

                        _lifeCounter--;
                        _paddle.Position = new Vector2(200, 660);
                        _listOfBalls.Add(new Ball(_ballTexture) { Position = new Vector2(262, 644) });
                        break;
                    }                    
                }
            }         
        }

        private void CheckIfBallsCollideWithAnyBlock()
        {
            foreach (Ball ball in _listOfBalls)
            {
                foreach (Block block in _listOfBlocks)
                {
                    if (ball.Rectangle.Intersects(block.Rectangle))
                    {
                        float distanceVertical;
                        float distanceHorizontal;

                        //Ball comes from upper left
                        if (ball.OldRectangle.Center.X <= block.Rectangle.Center.X &&
                            ball.OldRectangle.Center.Y <= block.Rectangle.Center.Y)
                        {
                            distanceVertical = Math.Abs((block.Rectangle.Top - ball.OldRectangle.Bottom) * ball.DirectionY);
                            distanceHorizontal = Math.Abs((block.Rectangle.Left - ball.OldRectangle.Right) * ball.DirectionX);
                        }
                        //Ball comes from upper right
                        else if (ball.OldRectangle.Center.X >= block.Rectangle.Center.X &&
                                 ball.OldRectangle.Center.Y <= block.Rectangle.Center.Y)
                        {
                            distanceVertical = Math.Abs((block.Rectangle.Top - ball.OldRectangle.Bottom) * ball.DirectionY);
                            distanceHorizontal = Math.Abs((ball.OldRectangle.Left - block.Rectangle.Right) * ball.DirectionX);
                        }
                        //Ball comes from bottom left
                        else if (ball.OldRectangle.Center.X <= block.Rectangle.Center.X &&
                                 ball.OldRectangle.Center.Y >= block.Rectangle.Center.Y)
                        {
                            distanceVertical = Math.Abs((ball.OldRectangle.Top - block.Rectangle.Bottom) * ball.DirectionY);
                            distanceHorizontal = Math.Abs((block.Rectangle.Left - ball.OldRectangle.Right) * ball.DirectionX);
                        }
                        //Ball comes from bottom right
                        else
                        {
                            distanceVertical = Math.Abs((ball.OldRectangle.Top - block.Rectangle.Bottom) * ball.DirectionY);
                            distanceHorizontal = Math.Abs((ball.OldRectangle.Left - block.Rectangle.Right) * ball.DirectionX);
                        }

                        if (distanceVertical > distanceHorizontal)
                            ball.DirectionX = ball.DirectionX * -1;
                        else
                            ball.DirectionY = ball.DirectionY * -1;

                        if (block.State == 3)
                            block.State = 2;
                        else if (block.State == 2)
                            block.State = 1;
                        else if (block.State == 1)
                        {
                            _score += 40;
                            _listOfBlocks.Remove(block);
                        }

                        _score += 10;

                        break;
                    }                  
                }              
            }
        }

        private void CheckIfBallsCollideWithPaddle()
        {
            foreach (Ball ball in _listOfBalls)
            {
                if (ball.Rectangle.Intersects(_paddle.Rectangle))
                {
                    ball.DirectionY = Math.Abs(ball.DirectionY) * -1;
                    _score += 10;
                }
            }        
        }

        private void CheckIfGameIsWon()
        {
            bool gameWonFlag = true;

            foreach (Block block in _listOfBlocks)
            {
                if (block.State != 4)
                {
                    gameWonFlag = false;
                }
            }

            if (gameWonFlag)
                _game.ChangeState(new VictoryState(_game, _graphicsDevice, _content, _score));
        }

        private void CheckIfGameOver()
        {
            if (_lifeCounter < 0)
                _game.ChangeState(new GameoverState(_game, _graphicsDevice, _content, _score));
        }

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            _lifeCounter = 2;
            _score = 0;

            _font = _content.Load<SpriteFont>("Fonts/Font");

            var gameStateBackgroundTexture = _content.Load<Texture2D>("Backgrounds/GameStateBackground");
            _gameStateBackground = new Background(gameStateBackgroundTexture);

            var paddleTextureStandard = _content.Load<Texture2D>("Objects/PaddleStandard");
            _paddle = new Paddle(paddleTextureStandard, new Vector2(200, 660));

            _ballTexture = _content.Load<Texture2D>("Objects/Ball");
            _listOfBalls.Add(new Ball(_ballTexture) { Position = new Vector2(262, 644) });
                
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

            _components = new List<Component>()
            {
                _gameStateBackground,
                _paddle,
            };

        } 
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            DrawLifeCounter(spriteBatch);
            DrawScore(spriteBatch);

            foreach (Ball ball in _listOfBalls)
                ball.Draw(gameTime, spriteBatch);

            foreach (Block block in _listOfBlocks)
                block.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void DrawScore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Score", new Vector2(370, 28), Color.Red, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);

            if (_score > 9999)
            {
                spriteBatch.DrawString(_font, _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
            else if (_score > 999)
            {
                spriteBatch.DrawString(_font, "0" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
            else if (_score > 99)
            {
                spriteBatch.DrawString(_font, "00" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
            else if (_score > 9)
            {
                spriteBatch.DrawString(_font, "000" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.DrawString(_font, "0000" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
        }

        private void DrawLifeCounter(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "1UP", new Vector2(90, 28), Color.Red, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);

            if (_lifeCounter > 99)
            {
                spriteBatch.DrawString(_font, _lifeCounter.ToString(), new Vector2(90, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
            else if (_lifeCounter > 9)
            {
                spriteBatch.DrawString(_font, "0" + _lifeCounter.ToString(), new Vector2(90, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.DrawString(_font, "00" + _lifeCounter.ToString(), new Vector2(90, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //TODO: Delete sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);

            foreach (Ball ball in _listOfBalls)
                ball.Update(gameTime);

            foreach (Block block in _listOfBlocks)
                block.Update(gameTime);

            CheckIfBallsCollideWithPaddle();
            CheckIfBallsCollideWithAnyBlock();
            CheckBalls();
            CheckIfGameOver();
            CheckIfGameIsWon();
        }

        
        #endregion
    }
}
