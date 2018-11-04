using System;
using System.Collections.Generic;
using System.IO;
using Arkanoid.Controls;
using Arkanoid.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private Texture2D _paddleTexture;
        private Texture2D _ballTexture;
        private Texture2D _blockTexture;
        private Texture2D _lifePowerUPTexture;
        private Texture2D _ballSpeedPowerUPTexture;
        private Texture2D _paddleSpeedPowerUPTexture;
        private List<Ball> _listOfBalls = new List<Ball> { };
        private List<Block> _listOfBlocks = new List<Block> { };
        private List<PowerUP> _listOfPowerUPs = new List<PowerUP> { };

        private SoundEffect _paddleDestroySE;
        private SoundEffect _gameOverSE;
        private SoundEffect _ballCollideWithPaddleSE;
        private SoundEffect _ballCollideWithDestructibleBlockSE;
        private SoundEffect _ballCollideWithIndestructibleBlockSE;
        #endregion

        #region Constructors
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            _lifeCounter = 2;
            _score = 0;

            _font = _content.Load<SpriteFont>("Fonts/Font");

            var gameStateBackgroundTexture = _content.Load<Texture2D>("Backgrounds/GameStateBackground");
            _gameStateBackground = new Background(gameStateBackgroundTexture);

            _paddleTexture = _content.Load<Texture2D>("Objects/PaddleStandard");
            _paddle = new Paddle(_paddleTexture, new Vector2(200, 660));

            _ballTexture = _content.Load<Texture2D>("Objects/Ball");
            _listOfBalls.Add(new Ball(_ballTexture) { Position = new Vector2(262, 644) });

            _lifePowerUPTexture = _content.Load<Texture2D>("Objects/LifePowerUP");
            _ballSpeedPowerUPTexture = _content.Load<Texture2D>("Objects/BallSpeedPowerUP");
            _paddleSpeedPowerUPTexture = _content.Load<Texture2D>("Objects/PaddleSpeedPowerUP");

            _blockTexture = _content.Load<Texture2D>("Objects/Block");

            _paddleDestroySE = _content.Load<SoundEffect>("SoundEffects/PaddleDestroy");
            _gameOverSE = _content.Load<SoundEffect>("SoundEffects/GameOver");
            _ballCollideWithPaddleSE = _content.Load<SoundEffect>("SoundEffects/BallCollideWithPaddle");
            _ballCollideWithDestructibleBlockSE = _content.Load<SoundEffect>("SoundEffects/BallCollideWithDestructibleBlock");
            _ballCollideWithIndestructibleBlockSE = _content.Load<SoundEffect>("SoundEffects/BallCollideWithIndestructibleBlock");

            readLevelFromFile(new StreamReader("../../../../Levels/level1.txt"));

            _components = new List<Component>()
            {
                _gameStateBackground,
                _paddle,
            };
        }
        #endregion

        #region Methods 
        private void CheckBalls()
        {
            if (_listOfBalls.Count > 0)
                foreach (Ball ball in _listOfBalls)
                    if (ball.Position.Y > 680)
                    {
                        _listOfBalls.Remove(ball);

                        _lifeCounter--;
                        _paddle.Position = new Vector2(200, 660);
                        _listOfBalls.Add(new Ball(_ballTexture) { Position = new Vector2(262, 644) });
                        _paddleDestroySE.Play();
                        break;
                    }                          
        }

        private void CheckIfBallsCollideWithAnyBlock()
        {
            foreach (Ball ball in _listOfBalls)
                foreach (Block block in _listOfBlocks)
                    if (ball.Rectangle.Intersects(block.Rectangle))
                    {
                        if (ball.Rectangle.Intersects(block.Top) && ball.DirectionY > 0)
                            ball.DirectionY *= -1;
                        else if (ball.Rectangle.Intersects(block.Bottom) && ball.DirectionY < 0)
                            ball.DirectionY *= -1;
                        else if (ball.Rectangle.Intersects(block.Left) && ball.DirectionX > 0)
                            ball.DirectionX *= -1;
                        else if (ball.Rectangle.Intersects(block.Right) && ball.DirectionY < 0)
                            ball.DirectionX *= -1;

                        if (block.State != 4)
                        {
                            Random rnd = new Random();

                            if (rnd.Next(1, 6) == 5)
                            {
                                int type = rnd.Next(0, 5);
                                if (type == 0)
                                    _listOfPowerUPs.Add(new PowerUP(_lifePowerUPTexture, PowerUP.Type.Life, rnd.Next(20, 51), true, new Vector2(block.Rectangle.Left + 10, block.Rectangle.Top)));
                                else if (type == 1)
                                    _listOfPowerUPs.Add(new PowerUP(_ballSpeedPowerUPTexture, PowerUP.Type.BallSpeed, rnd.Next(20, 51), true, new Vector2(block.Rectangle.Left + 10, block.Rectangle.Top)));
                                else if (type == 2)
                                    _listOfPowerUPs.Add(new PowerUP(_ballSpeedPowerUPTexture, PowerUP.Type.BallSpeed, rnd.Next(20, 51), false, new Vector2(block.Rectangle.Left + 10, block.Rectangle.Top)));
                                else if (type == 3)
                                    _listOfPowerUPs.Add(new PowerUP(_paddleSpeedPowerUPTexture, PowerUP.Type.PaddleSpeed, rnd.Next(20, 51), true, new Vector2(block.Rectangle.Left + 10, block.Rectangle.Top)));
                                else if (type == 4)
                                    _listOfPowerUPs.Add(new PowerUP(_paddleSpeedPowerUPTexture, PowerUP.Type.PaddleSpeed, rnd.Next(20, 51), false, new Vector2(block.Rectangle.Left + 10, block.Rectangle.Top)));
                            }
                        }

                        if (block.State == 3)
                        {
                            _ballCollideWithDestructibleBlockSE.Play();

                            block.State = 2;
                            _score += 10;
                        }
                        else if (block.State == 2)
                        {
                            _ballCollideWithDestructibleBlockSE.Play();

                            block.State = 1;
                            _score += 10;
                        }
                        else if (block.State == 1)
                        {
                            _ballCollideWithDestructibleBlockSE.Play();

                            _score += 50;
                            _listOfBlocks.Remove(block);
                            ball.SpeedCounter -= 2;
                        }
                        else if (block.State == 4)
                            _ballCollideWithIndestructibleBlockSE.Play();

                        ball.SpeedCounter++;

                        break;
                    }                                
        }

        private void CheckIfBallsCollideWithPaddle()
        {
            foreach (Ball ball in _listOfBalls)
                if (ball.Rectangle.Intersects(_paddle.Rectangle))
                {
                    _ballCollideWithPaddleSE.Play();

                    double paddleWidth = _paddle.Rectangle.Width;
                    double absoluteBallToPaddlePosition = ball.Rectangle.Center.X - _paddle.Rectangle.Left;

                    if(absoluteBallToPaddlePosition > paddleWidth)
                        absoluteBallToPaddlePosition = paddleWidth;
                    else if(absoluteBallToPaddlePosition < 0)
                        absoluteBallToPaddlePosition = 0;

                    double angle = Math.Round((absoluteBallToPaddlePosition * 110) / paddleWidth) + 35;

                    if (angle >= 90 && angle < 95)
                        angle = 95;
                    else if (angle < 90 && angle > 85)
                        angle = 85;

                    angle = Math.Round(angle / 5) * 5;

                    ball.DirectionX = Math.Cos(ConvertToRadians(angle)) * -1;
                    ball.DirectionY = Math.Sin(ConvertToRadians(angle)) * -1;

                    ball.SpeedCounter++;
                }    
        }

        private void CheckIfGameIsWon()
        {
            bool gameWonFlag = true;

            foreach (Block block in _listOfBlocks)
                if (block.State != 4)
                    gameWonFlag = false;

            if (gameWonFlag)
                _game.ChangeState(new VictoryState(_game, _graphicsDevice, _content, _score));
        }

        private void CheckIfGameOver()
        {
            if (_lifeCounter < 0)
            {
                _gameOverSE.Play();
                _game.ChangeState(new GameoverState(_game, _graphicsDevice, _content, _score));
            }
                
        }

        private void CheckPowerUPs()
        {
            foreach (PowerUP powerUP in _listOfPowerUPs)
            {
                if (powerUP.Rectangle.Intersects(_paddle.Rectangle))
                {
                    if (powerUP.State == PowerUP.Type.Life)
                        _lifeCounter++;
                    else if (powerUP.State == PowerUP.Type.BallSpeed && powerUP.IsGood == true)
                        foreach (Ball ball in _listOfBalls)
                            ball.Speed -= 0.2f;
                    else if (powerUP.State == PowerUP.Type.BallSpeed && powerUP.IsGood == false)
                        foreach (Ball ball in _listOfBalls)
                            ball.Speed += 0.1f;
                    else if (powerUP.State == PowerUP.Type.PaddleSpeed && powerUP.IsGood == true)
                        _paddle.Speed += 0.1f;
                    else if (powerUP.State == PowerUP.Type.PaddleSpeed && powerUP.IsGood == false)
                        _paddle.Speed -= 0.1f;

                    _listOfPowerUPs.Remove(powerUP);
                    break;
                }

                if (powerUP.Rectangle.Top >= _graphicsDevice.Viewport.Height)
                {
                    _listOfPowerUPs.Remove(powerUP);
                    break;
                }                
            }
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
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

            foreach (PowerUP powerUP in _listOfPowerUPs)
                powerUP.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void DrawScore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Score", new Vector2(370, 28), Color.Red, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);

            if (_score > 9999)
                spriteBatch.DrawString(_font, _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            else if (_score > 999)
                spriteBatch.DrawString(_font, "0" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            else if (_score > 99)
                spriteBatch.DrawString(_font, "00" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            else if (_score > 9)
                spriteBatch.DrawString(_font, "000" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            else
                spriteBatch.DrawString(_font, "0000" + _score.ToString(), new Vector2(370, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
        }

        private void DrawLifeCounter(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "1UP", new Vector2(90, 28), Color.Red, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);

            if (_lifeCounter > 99)
                spriteBatch.DrawString(_font, _lifeCounter.ToString(), new Vector2(90, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            else if (_lifeCounter > 9)
                spriteBatch.DrawString(_font, "0" + _lifeCounter.ToString(), new Vector2(90, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
            else
                spriteBatch.DrawString(_font, "00" + _lifeCounter.ToString(), new Vector2(90, 50), Color.White, (float)0.0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 1);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //TODO: Delete sprites if they're not needed
        }

        private void readLevelFromFile(StreamReader file)
        {
            string line;
            string[] lineDevided;
            char splitter = (char)59;
            int positionX = 20; //+40 each ; and reset after new line;
            int positionY = 180; //+20 each line

            while ((line = file.ReadLine()) != null)
            {
                positionX = 20;
                lineDevided = line.Split(splitter);

                for(int i = 0; i < lineDevided.Length; i++)
                {
                    if (Int32.Parse(lineDevided[i]) != 0)
                    {
                        var block = new Block(_blockTexture, Int32.Parse(lineDevided[i]))
                        { Position = new Vector2(positionX, positionY) };
                        _listOfBlocks.Add(block);
                    }
                    positionX += 40;
                }                
                positionY += 20;
            }

            file.Close();
        }

        public override void Update(GameTime gameTime)
        {
            for(int i = 0; i < 10 ; i++)
            {
                foreach (var component in _components)
                    component.Update(gameTime);

                foreach (Ball ball in _listOfBalls)
                    ball.Update(gameTime);

                foreach (Block block in _listOfBlocks)
                    block.Update(gameTime);

                foreach (PowerUP powerUP in _listOfPowerUPs)
                    powerUP.Update(gameTime);

                CheckIfBallsCollideWithPaddle();
                CheckIfBallsCollideWithAnyBlock();
                CheckBalls();
                CheckPowerUPs();
                CheckIfGameOver();
                CheckIfGameIsWon();
            }
      
        }
        #endregion
    }
}
