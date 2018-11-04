using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Objects
{
    public class PowerUP : Component
    {
        #region Fields
        private bool _isGood;
        private Vector2 _position;
        private double _speed;
        private Type _state;
        private Texture2D _texture;
        #endregion

        #region Enums
        public enum Type
        {
            Life,
            BallSpeed,
            PaddleSpeed,
        }
        #endregion

        #region Properties
        public bool IsGood { get => _isGood; }

        public Vector2 Position { get => _position; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        /* Possible states:
         0 - OneUP
         1 - Paddle speed up or speed down
         2 - Ball speed up or speed down
         */
        public Type State { get => _state; set => _state = value; }
        #endregion

        #region Constructors
        public PowerUP(Texture2D texture, Type state, int speed, bool isGood, Vector2 position)
        {
            _isGood = isGood;
            _position = position;
            _state = state;
            _speed = speed / 10;
            _texture = texture;
        }
        #endregion

        #region Methods
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_isGood == true)
                spriteBatch.Draw(_texture, Rectangle, Color.Green);
            else
                spriteBatch.Draw(_texture, Rectangle, Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            _position.Y = _position.Y + (float)_speed;
        }
        #endregion
    }
}
