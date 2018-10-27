using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.GameComponents
{
    class Paddle : Component
    {

        #region Fields  
        private Vector2 _paddlePosition;
        private Texture2D _paddle;
        #endregion

        #region Properties
        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _paddle.Width, _paddle.Height);
            }
        }
        #endregion

        #region Methods
        public Paddle(Texture2D texture)
        {
            _paddlePosition = new Vector2(350, 400);

            _paddle = new Texture2D(this.GraphicsDevice, 160, 40);
            Color[] color = new Color[160 * 40];
            for (int i = 0; i < color.Length - 1; i++)
            {
                color[i] = Color.Red;
            }
            paddle.SetData<Color>(color);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        #endregion
    }
}
