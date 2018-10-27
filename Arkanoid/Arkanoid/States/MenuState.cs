﻿using Arkanoid.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private SpriteFont _font;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            _font = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(300, 130),
                Text = "New Game",
            };
            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(300, 180),
                Text = "Load Game",
            };
            loadGameButton.Click += LoadGameButton_Click;

            var optionsButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(300, 230),
                Text = "Options",
            };
            optionsButton.Click += OptionsButton_Click;

            var quitButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(300, 280),
                Text = "Quit",
            };
            quitButton.Click += QuitButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                loadGameButton,
                optionsButton,
                quitButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            //Load Game
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new OptionsState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}