using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Objects
{
    public class Block : Component
    {
        #region Fields
        private Texture2D _texture;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }

        /* Possible states:
         * 1 - After collision block is destroyed
         * 2 - After collision block change state to 1
         * 3 - After collision block change state to 2
         * 4 - Block is indestructable and doesn't need to be destroyed to win level */
        public int State { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        #endregion

        #region Methods
        public Block(Texture2D texture)
        {
            _texture = texture;
            State = 1;
        }
        public Block(Texture2D texture, int state)
        {
            _texture = texture;
            State = state;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.Gray;

            if (State == 4)
                color = Color.Gray;
            else if (State == 3)
                color = Color.DarkRed;
            else if (State == 2)
                color = Color.DarkOrange;
            else if (State == 1)
                color = Color.DarkOliveGreen;
            else
            {
                throw new NotImplementedException();
            }

            spriteBatch.Draw(_texture, Rectangle, color);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Check collision and update State or destroy Block if(State == 1)
        }
        #endregion
    }
}
