using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Objects
{
    public class Paddle : Component
    {
        #region Fields
        private Texture2D _texture;
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
        #endregion

        #region Methods
        public Paddle(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Controling paddle, changing paddle after picking up various power ups
        }
        #endregion
    }
}
