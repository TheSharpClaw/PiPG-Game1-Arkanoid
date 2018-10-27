using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arkanoid.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.States
{
    public class OptionsState : State
    {
        private List<Component> _components;

        private Button _easyButton;
        private Button _mediumButton;
        private Button _hardButton;
        private Button _cancelButton;
        private Button _saveButton;

        private SpriteFont _font;

        public OptionsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            _font = _content.Load<SpriteFont>("Fonts/Font");

            _easyButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(175, 150),
                Text = "Easy",
            };
            _easyButton.Click += EasyButton_Click;

            _mediumButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(330, 150),
                Text = "Medium",
            };
            _mediumButton.Click += MediumButton_Click;

            _hardButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(485, 150),
                Text = "Hard",
            };
            _hardButton.Click += HardButton_Click;

            _cancelButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(255, 400),
                Text = "Cancel",
            };
            _cancelButton.Click += CancelButton_Click;

            _saveButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(405, 400),
                Text = "Save",
            };
            _saveButton.Click += SaveButton_Click;

            _components = new List<Component>()
            {
                _easyButton,
                _mediumButton,
                _hardButton,
                _cancelButton,
                _saveButton,
            };
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(_font, "AI Difficulty", new Vector2(175, 120), Color.Black);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void EasyButton_Click(object sender, EventArgs e)
        {
            _easyButton.IsClicked = true;
            _mediumButton.IsClicked = false;
            _hardButton.IsClicked = false;
        }

        private void HardButton_Click(object sender, EventArgs e)
        {
            _hardButton.IsClicked = true;
            _easyButton.IsClicked = false;
            _mediumButton.IsClicked = false;          
        }

        private void MediumButton_Click(object sender, EventArgs e)
        {
            _mediumButton.IsClicked = true;
            _easyButton.IsClicked = false;
            _hardButton.IsClicked = false;
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Remove sprites if they're not needed
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
