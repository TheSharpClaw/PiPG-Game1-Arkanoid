using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Objects
{
    public class TextBox : Component
    {
        #region Fields
        private SpriteFont _font;
        private int _score;
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

        public int Score { get => _score; set => _score = value; }

        public string Text { get; set; }

        public Color TextColor { get; set; }
        #endregion

        #region Methods
        public TextBox(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            Score = 0;
            Text = "Score: ";
            TextColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text + Score.ToString(), new Vector2(x, y), TextColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        #endregion
    }
}
