using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Objects
{
    public class Block : Component
    {
        #region Fields
        private Texture2D _texture;
        private int _state;
        private Color _color;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        /* Possible states:
         * 1 - After collision block is destroyed
         * 2 - After collision block change state to 1
         * 3 - After collision block change state to 2
         * 4 - Block is indestructable and doesn't need to be destroyed to win level */
        public int State { get => _state; set => _state = value; }    
        #endregion

        #region Methods
        public Block(Texture2D texture)
        {
            _texture = texture;
            _state = 1;
        }
        public Block(Texture2D texture, int state)
        {
            _texture = texture;
            _state = state;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _color = Color.Gray;

            if (State == 4)
                _color = Color.Gray;
            else if (State == 3)
                _color = Color.DarkRed;
            else if (State == 2)
                _color = Color.DarkOrange;
            else if (State == 1)
                _color = Color.DarkOliveGreen;

            spriteBatch.Draw(_texture, Rectangle, _color);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (State == 4)
                _color = Color.Gray;
            else if (State == 3)
                _color = Color.DarkRed;
            else if (State == 2)
                _color = Color.DarkOrange;
            else if (State == 1)
                _color = Color.DarkOliveGreen;
        }
        #endregion
    }
}
