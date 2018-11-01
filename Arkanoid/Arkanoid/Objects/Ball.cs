using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid.Objects
{
    public class Ball : Component
    {
        #region Fields
        private float _directionX;
        private float _directionY;
        private Rectangle _oldRectangle;
        private Vector2 _position;
        private float _speed;
        private Texture2D _texture;
        bool _toDestroyFlag;
        #endregion

        #region Properties
        public float DirectionX { get => _directionX; set => _directionX = value; }

        public float DirectionY { get => _directionY; set => _directionY = value; }

        public Rectangle OldRectangle { get => _oldRectangle; }

        public Vector2 Position { get => _position; set => _position = value; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
      
        public float Speed { get => _speed; set => _speed = value; }
        public bool ToDestroyFlag { get => _toDestroyFlag;}
        #endregion

        #region Methods
        public Ball(Texture2D texture)
        {
            _texture = texture;
            DirectionX = (float)Math.Sqrt(2);
            DirectionY = (float)Math.Sqrt(2);
            Speed = 2;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);
        }

        private void EdgeCollisions()
        {
            if (Position.X < 20)
            {
                DirectionX = DirectionX * -1;
            }
            if (Position.X > 541 - _texture.Width)
            {
                DirectionX = DirectionX * -1;
            }
            if (Position.Y < 120)
            {
                DirectionY = DirectionY * -1;
            }
            if (Position.Y > 681 - _texture.Height)
            {
                _toDestroyFlag = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _oldRectangle = Rectangle;

            Position = new Vector2(Position.X + (DirectionX * _speed), Position.Y + (DirectionY * _speed));
            EdgeCollisions();
        }

        
        #endregion
    }
}
