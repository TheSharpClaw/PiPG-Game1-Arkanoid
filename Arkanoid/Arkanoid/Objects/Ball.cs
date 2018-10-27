using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Objects
{
    public class Ball : Component
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
        public Ball(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {          
            spriteBatch.Draw(_texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: 1. Changing ball speed after 20 collisions (speeding up), 
            //      2. Add ball movement, 
            //      3. Deleting ball after colliding with bottom of screen.
        }
        #endregion
    }
}
