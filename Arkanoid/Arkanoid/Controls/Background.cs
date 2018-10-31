using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Controls
{
    class Background : Component
    {
        #region Fields
        private Texture2D _texture;
        #endregion

        #region Methods
        public Background(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        #endregion
    }
}
