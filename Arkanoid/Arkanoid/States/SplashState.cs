using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Arkanoid.States
{
    class SplashState : State
    {
        #region Fields
        private Texture2D _splash1;
        private Texture2D _splash2;
        private Texture2D _splash3;
        private Texture2D _splash4;

        private SpriteFont _font;

        private float _opacity;
        private float _fontOpacity;
        private int _fontOpacityFlag;
        private float _increment;

        private Song _splashScreenSong;
        #endregion

        #region Constructors
        public SplashState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            _splash1 = content.Load<Texture2D>("SplashScreen/SplashScreen1");
            _splash2 = content.Load<Texture2D>("SplashScreen/SplashScreen2");
            _splash3 = content.Load<Texture2D>("SplashScreen/SplashScreen3");
            _splash4 = content.Load<Texture2D>("SplashScreen/SplashScreen4");

            _font = content.Load<SpriteFont>("Fonts/Font");

            _opacity = 0;
            _fontOpacity = 0;
            _fontOpacityFlag = 0;
            _increment = 0.0001f;

            _splashScreenSong = content.Load<Song>("SoundEffects/SplashScreenAndMainMenu");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_splashScreenSong);
        }
        #endregion

        #region Methods
        private void CheckIfKeyPressed() {
            var keys = Keyboard.GetState().GetPressedKeys();

            if (_opacity == 1 && keys.Length > 0)
            {
                MediaPlayer.IsRepeating = false;
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));         
            }
            else if (keys.Length > 0)
            {
                _opacity = 0.99f;
                _increment = 0.005f;
            }  
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_splash1, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_splash2, new Vector2(0, 0), Color.White * _opacity);
            spriteBatch.Draw(_splash3, new Vector2(0, 0), Color.White * _opacity);
            spriteBatch.Draw(_splash4, new Vector2(0, 0), Color.White * _opacity);

            spriteBatch.DrawString(_font, "Press any key to continue", new Vector2(50, 360), Color.White * _fontOpacity, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 1);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        private void SplashOpacityIncrementation()
        {
            if (_opacity < 1)
            {
                _opacity += _increment;
                _increment += 0.00004f;
            }
            else
            {
                _opacity = 1;

                if (_opacity == 1)
                    StringFlashing();
            }
        }

        private void StringFlashing()
        {
            if (_fontOpacityFlag >= 45)
            {
                if (_fontOpacity == 1)
                    _fontOpacity = 0;
                else
                    _fontOpacity = 1;

                _fontOpacityFlag = 0;
            }
            _fontOpacityFlag++;
        }

        public override void Update(GameTime gameTime)
        {
            CheckIfKeyPressed();
            SplashOpacityIncrementation();
        }
        #endregion
    }
}
